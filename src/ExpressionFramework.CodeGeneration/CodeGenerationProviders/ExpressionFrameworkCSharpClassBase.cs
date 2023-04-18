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
                property.SetDefaultValueForBuilderClassConstructor(new Literal($"{Constants.Namespaces.DomainBuilders}.ExpressionBuilderFactory.CreateTyped<{typeName.GetGenericArguments()}>(new {Constants.Namespaces.Domain}.Expressions.TypedConstantExpression<{typeName.GetGenericArguments()}>({GetDefaultValue(typeName.GetGenericArguments())}))"));
            }
        }

        base.FixImmutableBuilderProperty(property, typeName);
    }

    protected override void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
    {
        var typedInterface = typeBaseBuilder.Interfaces.FirstOrDefault(x => x != null && x.WithoutGenerics() == typeof(ITypedExpression<>).WithoutGenerics())?.FixTypeName()?.Replace("ExpressionFramework.CodeGeneration.Models.", $"{Constants.Namespaces.Domain}.");
        if (!string.IsNullOrEmpty(typedInterface))
        {
            var key = $"{typeBaseBuilder.Namespace}.{typeBaseBuilder.Name}"; //TODO: Add a GetFullName() method to non-generic TypeBaseBuilder class, just like we have on the TypeBase class
            if (!_typedInterfaceMap.ContainsKey(key))
            {
                //Console.WriteLine($"Adding to typed interfacemap: {typeBaseBuilder.Name}");

                _typedInterfaceMap.Add(key, typedInterface);
                //typeBaseBuilder.AddMethods(new ClassMethodBuilder()
                //   .WithName("Build")
                //   .WithTypeName($"{typeof(ITypedExpression<>).WithoutGenerics()}Builder<{typedInterface.GetGenericArguments()}>")
                //   .AddLiteralCodeStatements("return BuildTyped();")
                //   .WithExplicitInterfaceName($"{Constants.Namespaces.Domain}.Contracts.ITypedExpressionBuilder<{typedInterface.GetGenericArguments()}>"))
                //   .AddInterfaces(typedInterface);
            }
        }
        else if (typeBaseBuilder.Namespace.ToString() == $"{Constants.Namespaces.DomainBuilders}.Expressions")
        {
            //Console.WriteLine($"Want to check interfacemap for builder: {typeBaseBuilder.Name}");
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
    private static string GetDefaultValue(string typeName)
        => typeName == "System.String"
            ? "string.Empty"
            : $"default({typeName})";
}
