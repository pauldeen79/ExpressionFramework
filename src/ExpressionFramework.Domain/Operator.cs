namespace ExpressionFramework.Domain;

public abstract partial record Operator
{
    public Result<bool> Evaluate(object? context, Expression leftExpression, Expression rightExpression)
    {
        var leftValueResult = leftExpression.Evaluate(context);
        if (!leftValueResult.IsSuccessful())
        {
            return Result<bool>.FromExistingResult(leftValueResult);
        }

        var rightValueResult = rightExpression.Evaluate(context);
        if (!rightValueResult.IsSuccessful())
        {
            return Result<bool>.FromExistingResult(rightValueResult);
        }

        return Evaluate(leftValueResult.Value, rightValueResult.Value);
    }

    protected abstract Result<bool> Evaluate(object? leftValue, object? rightValue);
}
