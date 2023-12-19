namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Aggregators;

[ExcludeFromCodeCoverage]
public class AggregatorModelFactory : ExpressionFrameworkModelClassBase
{
    public override string Path => Constants.Namespaces.DomainModels;

    public override object CreateModel()
        => CreateBuilderFactories(
            GetOverrideModels(typeof(Models.IAggregator)),
            new(
                Constants.Namespaces.DomainModels,
                nameof(AggregatorModelFactory),
                Constants.TypeNames.Aggregator,
                Constants.Namespaces.DomainModelsAggregators,
                Constants.Types.AggregatorModel,
                Constants.Namespaces.DomainAggregators
            )
        );
}
