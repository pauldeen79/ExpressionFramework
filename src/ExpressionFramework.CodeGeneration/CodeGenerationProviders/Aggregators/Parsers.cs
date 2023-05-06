namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Aggregators.Aggregators;

[ExcludeFromCodeCoverage]
public class Parsers : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserAggregatorResultParsers;

    public override object CreateModel()
        => GetOverrideModels(typeof(Models.IAggregator))
            .Select(x => CreateParserClass
            (
                x,
                Constants.Types.Aggregator,
                x.Name,
                Constants.Namespaces.DomainAggregators
            ).Build());
}
