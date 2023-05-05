namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionParsersFullyGenerated : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserExpressionResultParsers;

    public override object CreateModel()
        => GetOverrideModels(typeof(IExpression))
            .Where(x => !x.Name.StartsWithAny("TypedConstant", "TypedDelegate", "Delegate"))
            .Select(model => CreateParserClass
            (
                model,
                Constants.Types.Expression,
                model.Name.ReplaceSuffix(Constants.Types.Expression, string.Empty, StringComparison.InvariantCulture),
                Constants.Namespaces.DomainExpressions
            ).Build());
}
