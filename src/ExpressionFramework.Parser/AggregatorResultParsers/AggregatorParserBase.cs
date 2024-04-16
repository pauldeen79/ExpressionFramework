namespace ExpressionFramework.Parser.AggregatorResultParsers;

public abstract class AggregatorParserBase : IFunctionResultParser
{
    private readonly string _functionName;

    protected AggregatorParserBase(string functionName)
    {
        ArgumentGuard.IsNotNull(functionName, nameof(functionName));

        _functionName = functionName;
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ArgumentGuard.IsNotNull(functionParseResult, nameof(functionParseResult)).FunctionName.Equals(_functionName, StringComparison.OrdinalIgnoreCase)
            ? Result.FromExistingResult<object?>(DoParse(functionParseResult, evaluator, parser))
            : Result.Continue<object?>();

    protected abstract Result<Aggregator> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser);
}
