namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Aggregators;

[ExcludeFromCodeCoverage]
public class AggregatorBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Namespaces.DomainBuilders;

    public override object CreateModel()
        => CreateBuilderFactories(
            GetOverrideModels(typeof(Models.IAggregator)),
            new(
                Constants.Namespaces.DomainBuilders,
                nameof(AggregatorBuilderFactory),
                Constants.TypeNames.Aggregator,
                Constants.Namespaces.DomainBuildersAggregators,
                Constants.Types.AggregatorBuilder,
                Constants.Namespaces.DomainAggregators
            )
        );
}
