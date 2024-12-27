namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class Parsers : ExpressionFrameworkCSharpClassBase
{
    public Parsers(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.ParserEvaluatableResultParsers;

    public override async Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
    {
        var settings = CreateSettings();
        return (await GetOverrideModels(typeof(IEvaluatable)))
            .OnSuccess(result =>
                Result.Success(result.Value!.Select(x => CreateParserClass
                (
                    x,
                    Constants.Types.Evaluatable,
                    x.WithoutInterfacePrefix(),
                    Constants.Namespaces.DomainEvaluatables,
                    settings
                ).Build())));
    }
}
