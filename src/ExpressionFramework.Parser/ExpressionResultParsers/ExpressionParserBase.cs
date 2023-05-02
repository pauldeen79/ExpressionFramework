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
        => IsNameValid(functionParseResult.FunctionName)
            ? DoParse(functionParseResult, evaluator, parser)
            : Result<Expression>.Continue();

    protected virtual bool IsNameValid(string functionName)
        => functionName.ToUpperInvariant() == _functionName.ToUpperInvariant();

    protected abstract Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser);

    protected ITypedExpression<T> GetArgumentValueResult<T>(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ProcessArgumentResult<T>(argumentName, functionParseResult.GetArgumentValueResult(index, argumentName, functionParseResult.Context, evaluator, parser));

    protected ITypedExpression<T> GetArgumentValueResult<T>(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser, T defaultValue)
        => ProcessArgumentResult<T>(argumentName, functionParseResult.GetArgumentValueResult(index, argumentName, functionParseResult.Context, evaluator, parser, defaultValue));

    protected ITypedExpression<IEnumerable> GetTypedExpressionsArgumentValueResult(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var expressions = GetArgumentValueResult<IEnumerable>(functionParseResult, index, argumentName, evaluator, parser).EvaluateTyped(functionParseResult.Context);

        return new TypedDelegateExpression<IEnumerable>(_ => expressions.IsSuccessful()
            ? expressions.Value!.OfType<object>().Select(x => new DelegateExpression(_ => x)).Cast<Expression>()
            : new Expression[] { new TypedConstantResultExpression<object?>(Result<object?>.FromExistingResult(expressions)) });
    }

    protected Expression GetExpressionArgumentValueResult(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var expressionResult = GetArgumentValueResult<object?>(functionParseResult, index, argumentName, evaluator, parser).EvaluateTyped(functionParseResult.Context);

        return expressionResult.IsSuccessful()
            ? new DelegateExpression(_ => expressionResult.Value)
            : new TypedConstantResultExpression<object?>(expressionResult);
    }

    protected IEnumerable<Expression> GetExpressionsArgumentValueResult(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var expressions = GetArgumentValueResult<IEnumerable>(functionParseResult, index, argumentName, evaluator, parser).EvaluateTyped(functionParseResult.Context);

        return expressions.IsSuccessful()
            ? expressions.Value!.OfType<object>().Select(x => new DelegateExpression(_ => x)).Cast<Expression>()
            : new Expression[] { new TypedConstantResultExpression<object?>(Result<object?>.FromExistingResult(expressions)) };
    }

    private static ITypedExpression<T> ProcessArgumentResult<T>(string argumentName, Result<object?> argumentValueResult)
    {
        if (!argumentValueResult.IsSuccessful())
        {
            return new TypedConstantResultExpression<T>(Result<T>.FromExistingResult(argumentValueResult));
        }

        if (argumentValueResult.Value is not T t)
        {
            return new TypedConstantResultExpression<T>(Result<T>.Invalid($"{argumentName} is not of type {typeof(T).FullName}"));
        }

        return new TypedDelegateResultExpression<T>(_ => Result<T>.Success(t));
    }
}
