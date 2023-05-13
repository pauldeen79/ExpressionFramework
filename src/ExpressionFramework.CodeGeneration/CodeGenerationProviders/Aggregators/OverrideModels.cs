namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Aggregators;

[ExcludeFromCodeCoverage]
public class OverrideModels : ExpressionFrameworkModelClassBase
{
    public override string Path => Constants.Paths.AggregatorModels;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(Models.IAggregator), Constants.Namespaces.Domain);
    protected override string BaseClassBuilderNamespace => Constants.Namespaces.DomainModels;
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.None; // there are no properties in aggregators, so this is not necessary

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetOverrideModels(typeof(Models.IAggregator)),
            Constants.Namespaces.DomainAggregators,
            CurrentNamespace);
}
