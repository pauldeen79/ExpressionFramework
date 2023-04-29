namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class EvaluatableParsers : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserEvaluatableResultParsers;
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(IEvaluatable))
            .Select(x => CreateParserClass
            (
                x,
                "Evaluatable",
                x.Name,
                true,
                m => m.AddNotImplementedException()
            ).Build());
}
