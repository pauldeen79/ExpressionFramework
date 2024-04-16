namespace ExpressionFramework.Parser;

public class ExpressionFrameworkParser : IExpressionFrameworkParser
{
    private readonly IEnumerable<IExpressionResolver> _resolvers;

    public ExpressionFrameworkParser(IEnumerable<IExpressionResolver> resolvers)
    {
        ArgumentGuard.IsNotNull(resolvers, nameof(resolvers));

        _resolvers = resolvers;
    }

    public Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        ArgumentGuard.IsNotNull(functionParseResult, nameof(functionParseResult));
        ArgumentGuard.IsNotNull(evaluator, nameof(evaluator));
        ArgumentGuard.IsNotNull(parser, nameof(parser));

        return _resolvers
            .Select(x => x.Parse(functionParseResult, evaluator, parser))
            .FirstOrDefault(x => x.Status != ResultStatus.Continue)
                ?? Result.NotSupported<Expression>($"Unknown expression: {functionParseResult?.FunctionName}");
    }
}
