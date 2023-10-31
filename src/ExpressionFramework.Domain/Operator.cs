namespace ExpressionFramework.Domain;

public abstract partial record Operator
{
    public Result<bool> Evaluate(object? context, Expression leftExpression, Expression rightExpression)
    {
        var results = new[] { leftExpression, rightExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = Array.Find(results, x => !x.IsSuccessful());
        return nonSuccessfulResult != null
            ? Result.FromExistingResult<bool>(nonSuccessfulResult)
            : Evaluate(results[0].Value, results[1].Value);
    }

    protected abstract Result<bool> Evaluate(object? leftValue, object? rightValue);
}
