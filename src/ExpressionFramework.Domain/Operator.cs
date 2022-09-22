namespace ExpressionFramework.Domain;

public abstract partial record Operator
{
    public Result<bool> Evaluate(object? context, Expression leftExpression, Expression rightExpression)
    {
        var results = new[] { leftExpression, rightExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = results.FirstOrDefault(x => !x.IsSuccessful());
        return nonSuccessfulResult != null
            ? Result<bool>.FromExistingResult(nonSuccessfulResult)
            : Evaluate(results[0].Value, results[1].Value);
    }

    protected abstract Result<bool> Evaluate(object? leftValue, object? rightValue);
}
