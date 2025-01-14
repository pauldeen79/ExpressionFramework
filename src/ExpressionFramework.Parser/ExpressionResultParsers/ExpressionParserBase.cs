namespace ExpressionFramework.Parser.ExpressionResultParsers;

public abstract class ExpressionParserBase : IFunction, IExpressionResolver
{
    public Result<object?> Evaluate(FunctionCallContext context)
    {
        var result = ParseExpression(context);

        return result.IsSuccessful() && result.Status != ResultStatus.Continue
            ? result.Value?.Evaluate(context) ?? Result.Success<object?>(null)
            : Result.FromExistingResult<object?>(result);
    }

    public Result Validate(FunctionCallContext context)
        => Result.Success();

    public Result<Expression> ParseExpression(FunctionCallContext context)
    {
        context = ArgumentGuard.IsNotNull(context, nameof(context));

        return DoParse(context);
    }

    protected abstract Result<Expression> DoParse(FunctionCallContext context);

    protected static Result<Expression> ParseTypedExpression(Type expressionType, int index, string argumentName, FunctionCallContext context)
    {
        expressionType = ArgumentGuard.IsNotNull(expressionType, nameof(expressionType));
        context = ArgumentGuard.IsNotNull(context, nameof(context));

        var typeResult = context.FunctionCall.Name.GetGenericTypeResult();
        if (!typeResult.IsSuccessful())
        {
            return Result.FromExistingResult<Expression>(typeResult);
        }

        var valueResult = context.FunctionCall.GetArgumentValueResult(index, argumentName, context);
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
