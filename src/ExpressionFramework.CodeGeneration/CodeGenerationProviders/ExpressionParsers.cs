namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionParsers : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserExpressionResultParsers;

    public override object CreateModel()
        => GetOverrideModels(typeof(IExpression))
            .Select(model => CreateParserClass
            (
                model,
                Constants.Types.Expression,
                model.Name.ReplaceSuffix(Constants.Types.Expression, string.Empty, StringComparison.InvariantCulture),
                Constants.Namespaces.DomainExpressions
            ).Build());
}
