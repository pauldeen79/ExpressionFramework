namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Operators;

[ExcludeFromCodeCoverage]
public class Parsers(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : ExpressionFrameworkCSharpClassBase(pipelineService, csharpExpressionDumper)
{
    public override string Path => Constants.Paths.ParserOperatorResultParsers;

    public override async Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
    {
        var settings = CreateSettings();
        return (await GetOverrideModels(typeof(IOperator)))
            .OnSuccess(result =>
                Result.Success(result.Value!.Select(x => CreateParserClass
                (
                    x,
                    Constants.Types.Operator,
                    x.WithoutInterfacePrefix(),
                    Constants.Namespaces.DomainOperators,
                    settings
                ).Build())));
    }
}
