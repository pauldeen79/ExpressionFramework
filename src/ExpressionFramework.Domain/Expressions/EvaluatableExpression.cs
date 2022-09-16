namespace ExpressionFramework.Domain.Expressions;

public partial record EvaluatableExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var result = Condition.Evaluate(context);
        if (!result.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(result);
        }
        return Result<object?>.Success(result.Value);
    }

    public Result<bool> EvaluateAsBoolean(object? context)
        => Condition.Evaluate(context);
}

