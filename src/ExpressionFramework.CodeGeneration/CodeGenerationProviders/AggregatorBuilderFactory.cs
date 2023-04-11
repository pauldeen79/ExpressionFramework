namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AggregatorBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Namespaces.DomainBuilders;

    public override object CreateModel()
        => CreateBuilderFactoryModels(
            GetOverrideModels(typeof(IAggregator)),
            new(Constants.Namespaces.DomainBuilders,
            nameof(AggregatorBuilderFactory),
            $"{Constants.Namespaces.Domain}.Aggregator",
            $"{Constants.Namespaces.DomainBuilders}.Aggregators",
            "AggregatorBuilder",
            $"{Constants.Namespaces.Domain}.Aggregators"));
}
