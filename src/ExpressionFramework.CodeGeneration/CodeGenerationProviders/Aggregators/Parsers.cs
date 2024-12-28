namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Aggregators.Aggregators;

[ExcludeFromCodeCoverage]
public class Parsers(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : ExpressionFrameworkCSharpClassBase(pipelineService, csharpExpressionDumper)
{
    public override string Path => Constants.Paths.ParserAggregatorResultParsers;

    public override async Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
    {
        var settings = CreateSettings();
        return (await GetOverrideModels(typeof(Models.IAggregator)))
            .OnSuccess(result =>
                Result.Success(result.Value!.Select(
                    x => CreateParserClass(x, Constants.Types.Aggregator, x.WithoutInterfacePrefix(), Constants.Namespaces.DomainAggregators, settings)
                        .Build())
                )
            );
    }
}
