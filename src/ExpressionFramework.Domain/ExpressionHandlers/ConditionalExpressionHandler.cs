namespace ExpressionFramework.Domain.ExpressionHandlers;

public class ConditionalExpressionHandler : ExpressionHandlerBase<ConditionalExpression>
{
    private readonly IConditionEvaluatorProvider _provider;

    public ConditionalExpressionHandler(IConditionEvaluatorProvider provider)
        => _provider = provider;

    protected override async Task<Result<object?>> Evaluate(object? context, ConditionalExpression typedExpression, IExpressionEvaluator evaluator)
    {
        var evaluationResult = await _provider.Get(evaluator).Evaluate(context, typedExpression.Conditions);

        if (!evaluationResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(evaluationResult);
        }

        if (evaluationResult.Value)
        {
            return await evaluator.Evaluate(context, typedExpression.ResultExpression);
        }

        if (typedExpression.DefaultExpression != null)
        {
            return await evaluator.Evaluate(context, typedExpression.DefaultExpression);
        }

        return Result<object?>.Success(null);
    }
}
