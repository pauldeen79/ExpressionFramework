namespace ExpressionFramework.Parser.ExpressionResultParsers;

public abstract class ExpressionParserBase : IFunctionResultParser, IExpressionResolver
{
    private readonly string _functionName;

    protected ExpressionParserBase(string functionName)
    {
        _functionName = functionName;
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var result = Parse(functionParseResult, evaluator, parser);

        return result.IsSuccessful() && result.Status != ResultStatus.Continue
            ? result.Value?.Evaluate(context) ?? Result<object?>.Success(null)
            : Result<object?>.FromExistingResult(result);
    }

    public Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => functionParseResult.FunctionName.ToUpperInvariant() == _functionName.ToUpperInvariant()
            ? DoParse(functionParseResult, evaluator, parser)
            : Result<Expression>.Continue();

    protected abstract Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser);

    protected TypedDelegateResultExpression<T> GetArgumentValueResult<T>(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ProcessArgumentResult<T>(argumentName, functionParseResult.GetArgumentValueResult(index, argumentName, functionParseResult.Context, evaluator, parser));

    protected TypedDelegateResultExpression<T> GetArgumentValueResult<T>(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser, T defaultValue)
        => ProcessArgumentResult<T>(argumentName, functionParseResult.GetArgumentValueResult(index, argumentName, functionParseResult.Context, evaluator, parser, defaultValue));

    protected ITypedExpression<IEnumerable> GetTypedExpressionsArgumentValueResult(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var expressions = GetArgumentValueResult<IEnumerable>(functionParseResult, index, argumentName, evaluator, parser).Value.Invoke(functionParseResult.Context);

        return new TypedDelegateExpression<IEnumerable>(_ => expressions.IsSuccessful()
            ? expressions.Value!.OfType<object>().Select(x => new DelegateExpression(_ => x)).Cast<Expression>()
            : new Expression[] { new DelegateResultExpression(_ => Result<object?>.FromExistingResult(expressions)) });
    }

    protected IEnumerable<Expression> GetExpressionsArgumentValueResult(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var expressions = GetArgumentValueResult<IEnumerable>(functionParseResult, index, argumentName, evaluator, parser).Value.Invoke(functionParseResult.Context);

        return expressions.IsSuccessful()
            ? expressions.Value!.OfType<object>().Select(x => new DelegateExpression(_ => x)).Cast<Expression>()
            : new Expression[] { new DelegateResultExpression(_ => Result<object?>.FromExistingResult(expressions)) };
    }

    private static TypedDelegateResultExpression<T> ProcessArgumentResult<T>(string argumentName, Result<object?> argumentValueResult)
    {
        if (!argumentValueResult.IsSuccessful())
        {
            return new TypedDelegateResultExpression<T>(_ => Result<T>.FromExistingResult(argumentValueResult));
        }

        if (argumentValueResult.Value is not T t)
        {
            return new TypedDelegateResultExpression<T>(_ => Result<T>.Invalid($"{argumentName} is not of type{typeof(T).FullName}"));
        }

        return new TypedDelegateResultExpression<T>(_ => Result<T>.Success(t));
    }
}
