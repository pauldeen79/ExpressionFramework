namespace ExpressionFramework.Domain.ExpressionHandlers;

public class SwitchExpressionHandler : ExpressionHandlerBase<SwitchExpression>
{
    private readonly IConditionEvaluatorProvider _provider;

    public SwitchExpressionHandler(IConditionEvaluatorProvider provider)
        => _provider = provider;

    protected override async Task<Result<object?>> Handle(object? context, SwitchExpression typedExpression, IExpressionEvaluator evaluator)
    {
        var conditionEvaluator = _provider.Get(evaluator);
        foreach (var @case in typedExpression.Cases)
        {
            var caseResult = await conditionEvaluator.Evaluate(context, @case.Conditions);
            if (!caseResult.IsSuccessful())
            {
                return Result<object?>.FromExistingResult(caseResult);
            }
            if (caseResult.Value)
            {
                return await evaluator.Evaluate(context, @case.Expression);
            }
        }

        if (typedExpression.DefaultExpression != null)
        {
            return await evaluator.Evaluate(context, typedExpression.DefaultExpression);
        }

        return Result<object?>.Success(null);
    }
}
