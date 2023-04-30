namespace ExpressionFramework.Parser.ExpressionResultParsers;

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

    protected TypedDelegateResultExpression<T> GetArgumentValue<T>(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator)
    {
        var argumentValueResult = functionParseResult.GetArgumentValue(index, argumentName, functionParseResult.Context, evaluator);
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

    protected ITypedExpression<IEnumerable> GetTypedExpressionsArgumentValue(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator)
    {
        var expressions = GetArgumentValue<IEnumerable>(functionParseResult, 0, nameof(ChainedExpression.Expressions), evaluator).Value.Invoke(functionParseResult.Context);

        return new TypedDelegateExpression<IEnumerable>(_ => expressions.IsSuccessful()
            ? expressions.Value!.OfType<object>().Select(x => new DelegateExpression(_ => x)).Cast<Expression>()
            : new Expression[] { new DelegateResultExpression(_ => Result<object?>.FromExistingResult(expressions)) });
    }

    protected IEnumerable<Expression> GetExpressionsArgumentValue(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator)
    {
        var expressions = GetArgumentValue<IEnumerable>(functionParseResult, 0, nameof(ChainedExpression.Expressions), evaluator).Value.Invoke(functionParseResult.Context);

        return expressions.IsSuccessful()
            ? expressions.Value!.OfType<object>().Select(x => new DelegateExpression(_ => x)).Cast<Expression>()
            : new Expression[] { new DelegateResultExpression(_ => Result<object?>.FromExistingResult(expressions)) };
    }
}
