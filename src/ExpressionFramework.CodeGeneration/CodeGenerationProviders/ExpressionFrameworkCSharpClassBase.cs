﻿using System.Xml.Schema;

namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract partial class ExpressionFrameworkCSharpClassBase : CSharpClassBase
{
    public override bool RecurseOnDeleteGeneratedFiles => false;
    public override string DefaultFileName => string.Empty; // not used because we're using multiple files, but it's abstract so we need to fill ilt

    protected override bool CreateCodeGenerationHeader => true;
    protected override bool EnableNullableContext => true;
    protected override Type RecordCollectionType => typeof(IReadOnlyCollection<>);
    protected override Type RecordConcreteCollectionType => typeof(ReadOnlyValueCollection<>);
    protected override string FileNameSuffix => Constants.TemplateGenerated;
    protected override string ProjectName => Constants.ProjectName;
    protected override Type BuilderClassCollectionType => typeof(IEnumerable<>);
    protected override bool AddBackingFieldsForCollectionProperties => true;
    protected override bool AddPrivateSetters => true;
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.Shared;

    protected override void FixImmutableBuilderProperty(ClassPropertyBuilder property, string typeName)
    {
        if (typeName.WithoutProcessedGenerics().GetClassName() == typeof(ITypedExpression<>).WithoutGenerics().GetClassName())
        {
            var init = $"{Constants.Namespaces.DomainBuilders}.ExpressionBuilderFactory.CreateTyped<{typeName.GetGenericArguments()}>(source.{{0}})";
            property.ConvertSinglePropertyToBuilderOnBuilder
            (
                $"{Constants.Namespaces.Domain}.Contracts.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}Builder<{typeName.GetGenericArguments()}>",
                property.IsNullable
                    ? "_{1}Delegate = new (() => source.{0} == null ? null : " + init + ")"
                    : "_{1}Delegate = new (() => " + init + ")"
            );

            if (!property.IsNullable)
            {
                // Allow a default value which implements ITypedExpression<T>, using a default constant value
                property.SetDefaultValueForBuilderClassConstructor(new Literal($"{Constants.Namespaces.DomainBuilders}.ExpressionBuilderFactory.CreateTyped<{typeName.GetGenericArguments()}>(new {Constants.Namespaces.Domain}.Expressions.TypedConstantExpression<{typeName.GetGenericArguments()}>({typeName.GetGenericArguments().GetDefaultValue(property.IsNullable)}!))"));
            }
        }

        base.FixImmutableBuilderProperty(property, typeName);
    }

    protected override void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
    {
        var typedInterface = GetTypedInterface(typeBaseBuilder);
        if (!string.IsNullOrEmpty(typedInterface))
        {
            RegisterTypedInterface(typeBaseBuilder, typedInterface);
        }
        else if (typeBaseBuilder.Namespace.ToString() == $"{Constants.Namespaces.Domain}.Expressions")
        {
            AddCodeForTypedExpressionToExpressions(typeBaseBuilder);
        }
        else if (typeBaseBuilder.Namespace.ToString() == $"{Constants.Namespaces.DomainBuilders}.Expressions")
        {
            AddCodeForTypedExpressionToExpressionBuilders(typeBaseBuilder);
        }
    }

    private static string? GetTypedInterface<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase
    {
        var typedInterface = typeBaseBuilder.Interfaces.FirstOrDefault(x => x != null && x.WithoutProcessedGenerics() == typeof(ITypedExpression<>).WithoutGenerics());

        // This is a kind of hack for the fact that .net says the generic type argument of IEnumerable<T> is nullable.
        // ModelFramework is not extendable for this, so we are currently hacking this here.
        // Maybe it's an idea to add some sort of formatting function to CodeGenerationSettings, or even try to do this in the type formatting delegate that's already there? 
        if (typedInterface == "ExpressionFramework.CodeGeneration.Models.Contracts.ITypedExpression<System.Collections.Generic.IEnumerable<System.Object>>")
        {
            typedInterface = "ExpressionFramework.CodeGeneration.Models.Contracts.ITypedExpression<System.Collections.Generic.IEnumerable<System.Object?>>";
        }

        return typedInterface;
    }

    private void RegisterTypedInterface<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder, string typedInterface)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase
    {
        var key = typeBaseBuilder.GetFullName();
        if (!TypedInterfaceMap.ContainsKey(key))
        {
            TypedInterfaceMap.Add(key, typedInterface);
        }
    }

    private void AddCodeForTypedExpressionToExpressions<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase
    {
        string? typedInterface;
        var key = typeBaseBuilder.GetFullName();
        if (TypedInterfaceMap.TryGetValue(key, out typedInterface))
        {
            typeBaseBuilder.AddInterfaces($"{Constants.Namespaces.Domain}.Contracts.ITypedExpression<{typedInterface.GetGenericArguments()}>");
            if (!typeBaseBuilder.Name.ToString().StartsWithAny("TypedConstant", "TypedDelegate"))
            {
                typeBaseBuilder.AddMethods(
                    new ClassMethodBuilder()
                        .WithName("ToUntyped")
                        .WithTypeName("Expression")
                        .AddLiteralCodeStatements("return this;")
                );
            }
        }
        else if (key.EndsWith("Base"))
        {
            typeBaseBuilder.AddMethods(
                new ClassMethodBuilder()
                    .WithName("Evaluate")
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<object?>")
                    .WithOverride()
                    .AddParameter("context", typeof(object), isNullable: true)
                    .AddNotImplementedException()
            );

            BaseTypes.Add(typeBaseBuilder.GetFullName(), typeBaseBuilder);
        }

        if (!typeBaseBuilder.Name.ToString().EndsWith("Base")
            && typeBaseBuilder is ClassBuilder classBuilder
            && classBuilder.Constructors.Any()
            && BaseTypes.TryGetValue($"{typeBaseBuilder.GetFullName()}Base", out var baseType)
            && baseType.Properties.Any(x => x.TypeName.ToString().WithoutProcessedGenerics().GetClassName() == "ITypedExpression" || x.TypeName.ToString().GetClassName() == "Expression"))
        {
            // Add c'tor that uses T instead of ITypedExpression<T>, and calls the other overload.
            // This is needed pre .NET 7.0 because we can't use static implicit operators with generics.
            var ctor = classBuilder.Constructors.Last();
            classBuilder.AddConstructors(
                new ClassConstructorBuilder()
                    .AddParameters(
                        ctor.Parameters.Select(x => new ParameterBuilder()
                            .WithName(x.Name)
                            .WithTypeName(CreateTypeName(x))
                            .WithIsNullable(x.IsNullable)
                            .WithDefaultValue(x.DefaultValue)
                            )
                    )
                    .WithChainCall("this(" + string.Join(", ", ctor.Parameters.Select(x => CreateParameterSelection(x))) + ")")
                );
        }
    }

    private static string CreateParameterSelection(ParameterBuilder x)
    {
        if (x.TypeName.ToString().WithoutProcessedGenerics().GetClassName() == "ITypedExpression")
        {
            // we need the Value propery of Nullable<T> for value types...
            // for now, we only support int, long and boolean
            var suffix = x.TypeName.ToString().GetGenericArguments().In("System.Int32", "System.Int64", "System.Boolean", "int", "long", "bool")
                ? ".Value"
                : string.Empty;

            return x.IsNullable
                ? $"{x.Name.ToString().GetCsharpFriendlyName()} == null ? null : new TypedConstantExpression<{x.TypeName.ToString().GetGenericArguments()}>({x.Name.ToString().GetCsharpFriendlyName()}{suffix})"
                : $"new TypedConstantExpression<{x.TypeName.ToString().GetGenericArguments()}>({x.Name.ToString().GetCsharpFriendlyName()})";
        }

        if (x.TypeName.ToString().GetClassName() == "Expression")
        {
            return $"new ConstantExpression({x.Name.ToString().GetCsharpFriendlyName()})";
        }

        return x.Name.ToString().GetCsharpFriendlyName();
    }

    private static string CreateTypeName(ParameterBuilder x)
    {
        if (x.TypeName.ToString().WithoutProcessedGenerics().GetClassName() == "ITypedExpression")
        {
            return x.TypeName.ToString().GetGenericArguments();
        }

        if (x.TypeName.ToString().GetClassName() == "Expression")
        {
            // note that you might expect to check for the nullability of the property, but the Expression itself may be required although it's evaluation can result in null
            return "System.Object?";
        }

        return x.TypeName.ToString();
    }

    private void AddCodeForTypedExpressionToExpressionBuilders<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase
    {
        string? typedInterface;
        var buildTypedMethod = typeBaseBuilder.Methods.First(x => x.Name.ToString() == "BuildTyped");
        if (TypedInterfaceMap.TryGetValue(buildTypedMethod.TypeName.ToString().WithoutProcessedGenerics(), out typedInterface))
        {
            typeBaseBuilder.AddMethods
            (
                new ClassMethodBuilder()
                    .WithName("Build")
                    .WithTypeName($"{Constants.Namespaces.Domain}.Contracts.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{typedInterface.GetGenericArguments()}>")
                    .AddLiteralCodeStatements("return BuildTyped();")
                    .WithExplicitInterfaceName($"{Constants.Namespaces.Domain}.Contracts.ITypedExpressionBuilder<{typedInterface.GetGenericArguments()}>")
            )
            .AddInterfaces($"{Constants.Namespaces.Domain}.Contracts.ITypedExpressionBuilder<{typedInterface.GetGenericArguments()}>");
        }
    }

    protected Dictionary<string, string> TypedInterfaceMap { get; } = new();
    protected Dictionary<string, TypeBaseBuilder> BaseTypes { get; } = new();
}
