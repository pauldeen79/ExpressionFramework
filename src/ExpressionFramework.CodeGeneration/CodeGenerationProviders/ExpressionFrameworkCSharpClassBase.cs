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
        if (typeBaseBuilder.Name.ToString().StartsWithAny("TypedConstant", "TypedDelegate"))
        {
            return;
        }

        var typedInterface = typeBaseBuilder.Interfaces.FirstOrDefault(x => x != null && x.WithoutProcessedGenerics() == typeof(ITypedExpression<>).WithoutGenerics())?.FixTypeName();
        if (!string.IsNullOrEmpty(typedInterface))
        {
            var key = typeBaseBuilder.GetFullName();
            if (!_typedInterfaceMap.ContainsKey(key))
            {
                _typedInterfaceMap.Add(key, typedInterface);
            }
        }
        else if (typeBaseBuilder.Namespace.ToString() == $"{Constants.Namespaces.Domain}.Expressions")
        {
            var key = typeBaseBuilder.GetFullName();
            if (_typedInterfaceMap.TryGetValue(key, out typedInterface))
            {
                typeBaseBuilder.AddInterfaces($"{Constants.Namespaces.Domain}.Contracts.ITypedExpression<{typedInterface.GetGenericArguments()}>");
            }
        }
        else if (typeBaseBuilder.Namespace.ToString() == $"{Constants.Namespaces.DomainBuilders}.Expressions")
        {
            var buildTypedMethod = typeBaseBuilder.Methods.First(x => x.Name.ToString() == "BuildTyped");
            if (_typedInterfaceMap.TryGetValue(buildTypedMethod.TypeName.ToString(), out typedInterface))
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
    }

    private readonly Dictionary<string, string> _typedInterfaceMap = new();
}
