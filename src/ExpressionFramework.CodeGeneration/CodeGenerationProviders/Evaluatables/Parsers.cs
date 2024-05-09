namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class Parsers : ExpressionFrameworkCSharpClassBase
{
    public Parsers(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.ParserEvaluatableResultParsers;

    public override async Task<IEnumerable<TypeBase>> GetModel()
    {
        var settings = CreateSettings();
        return (await GetOverrideModels(typeof(IEvaluatable)))
            .Select(x => CreateParserClass
            (
                x,
                Constants.Types.Evaluatable,
                x.WithoutInterfacePrefix(),
                Constants.Namespaces.DomainEvaluatables,
                settings
            ).Build());
    }
}
