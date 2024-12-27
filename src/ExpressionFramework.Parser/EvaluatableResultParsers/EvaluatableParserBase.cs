namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public abstract class EvaluatableParserBase : IFunctionResultParser
{
    private readonly string _functionName;

    protected EvaluatableParserBase(string functionName)
    {
        ArgumentGuard.IsNotNull(functionName, nameof(functionName));

        _functionName = functionName;
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ArgumentGuard.IsNotNull(functionParseResult, nameof(functionParseResult)).FunctionName.Equals(_functionName, StringComparison.OrdinalIgnoreCase)
            ? Result.FromExistingResult<object?>(DoParse(functionParseResult, evaluator, parser))
            : Result.Continue<object?>();

    protected abstract Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser);
}
