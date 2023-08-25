namespace ExpressionFramework.Parser.OperatorResultParsers;

public abstract class OperatorParserBase : IFunctionResultParser
{
    private readonly string _functionName;

    protected OperatorParserBase(string functionName)
    {
        _functionName = functionName ?? throw new ArgumentNullException(nameof(functionName));
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => (functionParseResult ?? throw new ArgumentNullException(nameof(functionParseResult))).FunctionName.ToUpperInvariant() == _functionName.ToUpperInvariant()
            ? Result<object?>.FromExistingResult(DoParse(functionParseResult, evaluator, parser))
            : Result<object?>.Continue();

    protected abstract Result<Operator> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser);
}
