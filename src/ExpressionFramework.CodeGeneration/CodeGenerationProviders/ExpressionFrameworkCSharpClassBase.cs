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
    protected override string ProjectName => Constants.ProjectName;
    protected override Type BuilderClassCollectionType => typeof(IEnumerable<>);
    protected override bool AddBackingFieldsForCollectionProperties => true;
    protected override bool AddPrivateSetters => true;
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.Shared;

    protected override void FixImmutableBuilderProperty(ClassPropertyBuilder property, string typeName)
    {
        if (typeName.WithoutProcessedGenerics().GetClassName() == typeof(ITypedExpression<>).WithoutGenerics().GetClassName())
        {
            var init = $"{Constants.Namespaces.DomainBuilders}.{nameof(ExpressionBuilderFactory)}.CreateTyped<{typeName.GetGenericArguments()}>(source.{{0}})";
            property.ConvertSinglePropertyToBuilderOnBuilder
            (
                $"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}Builder<{typeName.GetGenericArguments()}>",
                property.IsNullable
                    ? "_{1}Delegate = new (() => source.{0} == null ? null : " + init + ")"
                    : "_{1}Delegate = new (() => " + init + ")"
            );

            if (!property.IsNullable)
            {
                // Allow a default value which implements ITypedExpression<T>, using a default constant value
                property.SetDefaultValueForBuilderClassConstructor(new Literal($"{Constants.Namespaces.DomainBuilders}.{nameof(ExpressionBuilderFactory)}.CreateTyped<{typeName.GetGenericArguments()}>(new {Constants.Namespaces.DomainExpressions}.TypedConstantExpression<{typeName.GetGenericArguments()}>({typeName.GetGenericArguments().GetDefaultValue(property.IsNullable)}!))"));
            }
        }
        else if (typeName.WithoutProcessedGenerics().GetClassName() == typeof(IMultipleTypedExpressions<>).WithoutGenerics().GetClassName())
        {
            // This is an ugly hack to transform IMultipleTypedExpression<T> in the code generation model to IEnumerable<ITypedExpression<T>> in the domain model.
            var init = $"{Constants.Namespaces.DomainBuilders}.{nameof(ExpressionBuilderFactory)}.CreateTyped<{typeName.GetGenericArguments()}>(x)";
            property.ConvertCollectionPropertyToBuilderOnBuilder
            (
                false,
                RecordConcreteCollectionType.WithoutGenerics(),
                $"{typeof(IEnumerable<>).WithoutGenerics()}<{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}Builder<{typeName.GetGenericArguments()}>>",
                "{0} = source.{0}.Select(x => " + init + ").ToList()",
                builderCollectionTypeName: BuilderClassCollectionType.WithoutGenerics()
            );
            property.WithTypeName($"{typeof(IEnumerable<>).WithoutGenerics()}<{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{typeName.GetGenericArguments()}>>");
        }
        else
        {
            base.FixImmutableBuilderProperty(property, typeName);
        }
    }

    protected override void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
    {
        var typedInterface = GetTypedInterface(typeBaseBuilder);
        if (!string.IsNullOrEmpty(typedInterface))
        {
            RegisterTypedInterface(typeBaseBuilder, typedInterface);
        }
        else if (typeBaseBuilder.Namespace.ToString() == Constants.Namespaces.DomainExpressions)
        {
            AddCodeForTypedExpressionToExpressions(typeBaseBuilder);
        }
        else if (typeBaseBuilder.Namespace.ToString() == Constants.Namespaces.DomainBuildersExpressions)
        {
            AddCodeForTypedExpressionToExpressionBuilders(typeBaseBuilder);
        }
    }

    private string? GetTypedInterface<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase
    {
        var typedInterface = typeBaseBuilder.Interfaces.FirstOrDefault(x => x != null && x.WithoutProcessedGenerics() == typeof(ITypedExpression<>).WithoutGenerics());

        // This is a kind of hack for the fact that .net says the generic type argument of IEnumerable<T> is nullable.
        // ModelFramework is not extendable for this, so we are currently hacking this here.
        // Maybe it's an idea to add some sort of formatting function to CodeGenerationSettings, or even try to do this in the type formatting delegate that's already there? 
        if (typedInterface == $"{CodeGenerationRootNamespace}.Models.Contracts.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{typeof(IEnumerable<>).WithoutGenerics()}<{typeof(object).FullName}>>")
        {
            typedInterface = $"{CodeGenerationRootNamespace}.Models.Contracts.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{typeof(IEnumerable<>).WithoutGenerics()}<{typeof(object).FullName}?>>";
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
            typeBaseBuilder.AddInterfaces($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{typedInterface.GetGenericArguments()}>");
            if (!typeBaseBuilder.Name.ToString().StartsWithAny("TypedConstant", "TypedDelegate"))
            {
                typeBaseBuilder.AddMethods(
                    new ClassMethodBuilder()
                        .WithName("ToUntyped")
                        .WithTypeName(Constants.Types.Expression)
                        .AddLiteralCodeStatements("return this;")
                );
            }
        }
        else if (key.EndsWith("Base"))
        {
            typeBaseBuilder.AddMethods(
                new ClassMethodBuilder()
                    .WithName("Evaluate")
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
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
            && baseType.Properties.Any(
                x => x.TypeName.ToString().WithoutProcessedGenerics().GetClassName() == typeof(ITypedExpression<>).WithoutGenerics().GetClassName()
                  || x.TypeName.ToString().GetClassName() == Constants.Types.Expression)
            )
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
                    .WithChainCall("this(" + string.Join(", ", ctor.Parameters.Select(CreateParameterSelection)) + ")")
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
                    .WithTypeName($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{typedInterface.GetGenericArguments()}>")
                    .AddLiteralCodeStatements("return BuildTyped();")
                    .WithExplicitInterfaceName($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}Builder<{typedInterface.GetGenericArguments()}>")
            )
            .AddInterfaces($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}Builder<{typedInterface.GetGenericArguments()}>");
        }
    }

    protected Dictionary<string, string> TypedInterfaceMap { get; } = new();
    protected Dictionary<string, TypeBaseBuilder> BaseTypes { get; } = new();

    protected static bool IsSupportedExpressionForGeneratedParser(ITypeBase t)
        => !t.Name.StartsWith("TypedDelegate")
        && !t.GenericTypeArguments.Any()
        && t.Properties.All(IsSupported);

    protected ClassBuilder CreateParserClass(ITypeBase typeBase, string type, string name, bool addParser, Action<ClassMethodBuilder> methodDelegate)
        => new ClassBuilder()
            .WithNamespace(CurrentNamespace)
            .WithName($"{typeBase.Name}Parser")
            .WithBaseClass($"{type}ParserBase")
            .AddConstructors(new[]
            {
                new ClassConstructorBuilder()
                    .AddParameter("parser", typeof(IExpressionParser))
                    .WithChainCall($"base(parser, {name.CsharpFormat()})")
            }.Where(_ => addParser))
            .AddConstructors(new[]
            {
                new ClassConstructorBuilder()
                    .WithChainCall($"base({name.CsharpFormat()})")
            }.Where(_ => !addParser))

            .AddMethods(new ClassMethodBuilder()
                .WithName("DoParse")
                .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.{type}>")
                .AddParameter("functionParseResult", typeof(FunctionParseResult))
                .AddParameter("evaluator", typeof(IFunctionParseResultEvaluator))
                .WithProtected()
                .WithOverride()
                .With(methodDelegate)
            );

    private static bool IsSupported(IClassProperty p)
        => p.TypeName.WithoutProcessedGenerics().GetClassName().In(Constants.Types.Expression, Constants.Types.ITypedExpression)
        || p.TypeName == $"{Constants.Namespaces.DomainContracts}.{Constants.Types.ITypedExpression}<{typeof(IEnumerable).FullName}>"
        || p.TypeName == $"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.{Constants.Types.Expression}>";

    private static string CreateParameterSelection(ParameterBuilder x)
    {
        if (x.TypeName.ToString().WithoutProcessedGenerics().GetClassName() == typeof(ITypedExpression<>).WithoutGenerics().GetClassName() && x.Name.ToString() != "predicateExpression")
        {
            // we need the Value propery of Nullable<T> for value types... (except for predicate expressions, those still have to be injected using ITypedExpression<bool>)
            // for now, we only support int, long and boolean
            var suffix = x.TypeName.ToString().GetGenericArguments().In("System.Int32", "System.Int64", "System.Boolean", "int", "long", "bool")
                ? ".Value"
                : string.Empty;

            return x.IsNullable
                ? $"{x.Name.ToString().GetCsharpFriendlyName()} == null ? null : new TypedConstantExpression<{x.TypeName.ToString().GetGenericArguments()}>({x.Name.ToString().GetCsharpFriendlyName()}{suffix})"
                : $"new TypedConstantExpression<{x.TypeName.ToString().GetGenericArguments()}>({x.Name.ToString().GetCsharpFriendlyName()})";
        }

        if (x.TypeName.ToString().GetClassName() == Constants.Types.Expression)
        {
            return $"new ConstantExpression({x.Name.ToString().GetCsharpFriendlyName()})";
        }

        return x.Name.ToString().GetCsharpFriendlyName();
    }

    private static string CreateTypeName(ParameterBuilder x)
    {
        if (x.TypeName.ToString().WithoutProcessedGenerics().GetClassName() == typeof(ITypedExpression<>).WithoutGenerics().GetClassName())
        {
            if (x.Name.ToString() == "predicateExpression")
            {
                // hacking here... we only want to allow to inject the typed expression
                return x.TypeName.ToString();
            }
            else
            {
                return x.TypeName.ToString().GetGenericArguments();
            }
        }

        if (x.TypeName.ToString().GetClassName() == Constants.Types.Expression)
        {
            // note that you might expect to check for the nullability of the property, but the Expression itself may be required although it's evaluation can result in null
            return $"{typeof(object).FullName}?";
        }

        return x.TypeName.ToString();
    }

    public CodeGenerationSettings GetSettings(CodeGenerationSettings settings)
        => string.IsNullOrEmpty(LastGeneratedFilesFileName)
            ? settings.ForScaffolding()
            : settings.ForGeneration();
}
