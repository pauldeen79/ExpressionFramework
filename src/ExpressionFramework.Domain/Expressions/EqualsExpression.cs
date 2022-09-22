namespace ExpressionFramework.Domain.Expressions;

public partial record EqualsExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var results = new[] { FirstExpression, SecondExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = results.FirstOrDefault(x => !x.IsSuccessful());
        if (nonSuccessfulResult != null)
        {
            return nonSuccessfulResult;
        }

        return Result<object?>.Success(EqualsOperator.IsValid(results[0], results[1]));
    }
}
