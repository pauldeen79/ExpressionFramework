namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideOperatorBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.OperatorBuilders;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IOperator), Constants.Namespaces.Domain);
    protected override string BaseClassBuilderNamespace => Constants.Namespaces.DomainBuilders;
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.None; // there are no properties in operators, so this is not necessary

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetOverrideModels(typeof(IOperator)),
            Constants.Namespaces.DomainOperators,
            CurrentNamespace);
}
