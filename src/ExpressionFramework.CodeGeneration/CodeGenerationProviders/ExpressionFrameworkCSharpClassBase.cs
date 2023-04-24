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
                property.SetDefaultValueForBuilderClassConstructor(new Literal($"{Constants.Namespaces.DomainBuilders}.ExpressionBuilderFactory.CreateTyped<{typeName.GetGenericArguments()}>(new {Constants.Namespaces.Domain}.Expressions.TypedConstantExpression<{typeName.GetGenericArguments()}>({typeName.GetGenericArguments().GetDefaultValue(property.IsNullable)}))"));
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
        }
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
}
