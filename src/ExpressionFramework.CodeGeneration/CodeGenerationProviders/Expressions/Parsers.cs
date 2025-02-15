namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Expressions;

[ExcludeFromCodeCoverage]
public class Parsers(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : ExpressionFrameworkCSharpClassBase(pipelineService, csharpExpressionDumper)
{
    public override string Path => Constants.Paths.ParserExpressionResultParsers;

    public override async Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
    {
        var settings = CreateSettings();
        return (await GetOverrideModels(typeof(Models.IExpression)))
            .OnSuccess(result =>
                Result.Success(result.Value!.Select(x => CreateParserClass
                (
                    x,
                    Constants.Types.Expression,
                    x.WithoutInterfacePrefix().ReplaceSuffix(Constants.Types.Expression, string.Empty, StringComparison.InvariantCulture),
                    Constants.Namespaces.DomainExpressions,
                    settings
                ).Build())));
    }
}
