namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionParsersScaffolded : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserExpressionResultParsers;
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(IExpression))
            .Where(x => !IsSupportedExpressionForGeneratedParser(x))
            .Select(x => CreateParserClass
            (
                x,
                "Expression",
                x.Name.ReplaceSuffix("Expression", string.Empty, StringComparison.InvariantCulture),
                true,
                m => m.AddNotImplementedException()
            ).Build());
}
