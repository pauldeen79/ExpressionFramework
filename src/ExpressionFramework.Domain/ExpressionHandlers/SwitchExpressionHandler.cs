namespace ExpressionFramework.Domain.ExpressionHandlers;

public class SwitchExpressionHandler : ExpressionHandlerBase<SwitchExpression>
{
    protected override async Task<Result<object?>> Handle(object? context, SwitchExpression typedExpression, IExpressionEvaluator evaluator)
    {
        foreach (var @case in typedExpression.Cases)
        {
            var conditionalExpression = new ConditionalExpression(@case.Conditions, @case.Expression, null);
            var caseResult = await evaluator.Evaluate(context, conditionalExpression);
            if (!caseResult.IsSuccessful())
            {
                return Result<object?>.FromExistingResult(caseResult);
            }
            if (caseResult.HasValue)
            {
                return caseResult;
            }
        }

        if (typedExpression.DefaultExpression != null)
        {
            return await evaluator.Evaluate(context, typedExpression.DefaultExpression);
        }

        return Result<object?>.Success(null);
    }
}
