namespace ExpressionFramework.Parser.ExpressionResultParsers;

public abstract class ExpressionParserBase : IFunctionResultParser, IExpressionResolver
{
    private readonly string _functionName;

    protected ExpressionParserBase(string functionName)
    {
        ArgumentGuard.IsNotNull(functionName, nameof(functionName));

        _functionName = functionName;
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var result = Parse(functionParseResult, evaluator, parser);

        return result.IsSuccessful() && result.Status != ResultStatus.Continue
            ? result.Value?.Evaluate(context) ?? Result.Success<object?>(null)
            : Result.FromExistingResult<object?>(result);
    }

    public Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        functionParseResult = ArgumentGuard.IsNotNull(functionParseResult, nameof(functionParseResult));

        return IsFunctionValid(functionParseResult)
            ? DoParse(functionParseResult, evaluator, parser)
            : Result.Continue<Expression>();
    }

    protected virtual bool IsNameValid(string functionName)
        => ArgumentGuard.IsNotNull(functionName, nameof(functionName)).Equals(_functionName, StringComparison.OrdinalIgnoreCase);

    protected virtual bool IsFunctionValid(FunctionParseResult functionParseResult)
        => IsNameValid(ArgumentGuard.IsNotNull(functionParseResult, nameof(functionParseResult)).FunctionName);

    protected abstract Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser);

    protected Result<Expression> ParseTypedExpression(Type expressionType, int index, string argumentName, FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        expressionType = ArgumentGuard.IsNotNull(expressionType, nameof(expressionType));
        functionParseResult = ArgumentGuard.IsNotNull(functionParseResult, nameof(functionParseResult));

        var typeResult = functionParseResult.FunctionName.GetGenericTypeResult();
        if (!typeResult.IsSuccessful())
        {
            return Result.FromExistingResult<Expression>(typeResult);
        }

        var valueResult = functionParseResult.GetArgumentValueResult(index, argumentName, functionParseResult.Context, evaluator, parser);
        if (!valueResult.IsSuccessful())
        {
            return Result.FromExistingResult<Expression>(valueResult);
        }

#pragma warning disable CA1031 // Do not catch general exception types
        try
        {
            return Result.Success((Expression)Activator.CreateInstance(expressionType.MakeGenericType(typeResult.Value!), valueResult.Value));
        }
        catch (Exception ex)
        {
            return Result.Invalid<Expression>($"Could not create {expressionType.Name.Replace("`1", string.Empty)}. Error: {ex.Message}");
        }
#pragma warning restore CA1031 // Do not catch general exception types
    }
}
