namespace ExpressionFramework.Parser.OperatorResultParsers;

public abstract class OperatorParserBase : IFunctionResultParser
{
    private readonly string _functionName;

    protected OperatorParserBase(string functionName)
    {
        ArgumentGuard.IsNotNull(functionName, nameof(functionName));

        _functionName = functionName;
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ArgumentGuard.IsNotNull(functionParseResult, nameof(functionParseResult)).FunctionName.Equals(_functionName, StringComparison.OrdinalIgnoreCase)
            ? Result.FromExistingResult<object?>(DoParse(functionParseResult, evaluator, parser))
            : Result.Continue<object?>();

    protected abstract Result<Operator> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser);
}
