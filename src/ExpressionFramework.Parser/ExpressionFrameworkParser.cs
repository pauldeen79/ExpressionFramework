namespace ExpressionFramework.Parser;

public class ExpressionFrameworkParser : IExpressionFrameworkParser
{
    private readonly IEnumerable<IExpressionResolver> _resolvers;

    public ExpressionFrameworkParser(IEnumerable<IExpressionResolver> resolvers)
    {
        _resolvers = resolvers;
    }

    public Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => _resolvers
            .Select(x => x.Parse(functionParseResult, evaluator, parser))
            .FirstOrDefault(x => x.Status != ResultStatus.Continue)
                ?? Result<Expression>.NotSupported($"Unknown expression: {functionParseResult?.FunctionName}");
}
