namespace ExpressionFramework.Domain;

public abstract partial record Operator
{
    public Result<bool> Evaluate(object? context, Expression leftExpression, Expression rightExpression)
    {
        var results = new[] { leftExpression, rightExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = results.FirstOrDefault(x => !x.IsSuccessful());
        if (nonSuccessfulResult != null)
        {
            return Result<bool>.FromExistingResult(nonSuccessfulResult);
        }

        return Evaluate(results[0].Value, results[1].Value);
    }

    protected abstract Result<bool> Evaluate(object? leftValue, object? rightValue);
}
