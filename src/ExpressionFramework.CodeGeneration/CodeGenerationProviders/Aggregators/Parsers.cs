namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Aggregators.Aggregators;

[ExcludeFromCodeCoverage]
public class Parsers : ExpressionFrameworkCSharpClassBase
{
    public Parsers(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.ParserAggregatorResultParsers;

    public override async Task<IEnumerable<TypeBase>> GetModel()
    {
        var settings = CreateSettings();
        return (await GetOverrideModels(typeof(Models.IAggregator)))
            .Select(x => CreateParserClass(x, Constants.Types.Aggregator, x.WithoutInterfacePrefix(), Constants.Namespaces.DomainAggregators, settings).Build());
    }
}
