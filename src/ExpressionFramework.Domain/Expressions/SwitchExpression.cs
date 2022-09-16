namespace ExpressionFramework.Domain.Expressions;

public partial record SwitchExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        foreach (var @case in Cases)
        {
            var conditionalExpression = new ConditionalExpression(@case.Condition, @case.Expression, null);
            var caseResult = conditionalExpression.Evaluate(context);
            if (!caseResult.IsSuccessful())
            {
                return Result<object?>.FromExistingResult(caseResult);
            }
            if (caseResult.HasValue)
            {
                return caseResult;
            }
        }

        if (DefaultExpression != null)
        {
            return DefaultExpression.Evaluate(context);
        }

        return Result<object?>.Success(null);
    }
}
