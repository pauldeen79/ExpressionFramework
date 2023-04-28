namespace ExpressionFramework.Parser.FunctionResultParsers;

public abstract class ExpressionParserBase : IFunctionResultParser, IExpressionResolver
{
    private readonly string _functionName;
    protected IExpressionParser Parser { get; }

    protected ExpressionParserBase(IExpressionParser parser, string functionName)
    {
        Parser = parser;
        _functionName = functionName;
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator)
    {
        var result = Parse(functionParseResult, evaluator);
        return result.IsSuccessful() && result.Status != ResultStatus.Continue
            ? result.Value!.Evaluate(context)
            : Result<object?>.FromExistingResult(result);
    }

    public Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
        => functionParseResult.FunctionName.ToUpperInvariant() == _functionName.ToUpperInvariant()
            ? DoParse(functionParseResult, evaluator)
            : Result<Expression>.Continue();

    protected abstract Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator);
}
