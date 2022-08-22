namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class ConditionalExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    private readonly IConditionEvaluatorProvider _conditionEvaluatorProvider;

    public ConditionalExpressionEvaluatorProvider(IConditionEvaluatorProvider conditionEvaluatorProvider)
        => _conditionEvaluatorProvider = conditionEvaluatorProvider;

    public bool TryEvaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator, out object? result)
    {
        if (expression is not IConditionalExpression conditionalExpression)
        {
            result = default;
            return false;
        }

        if (_conditionEvaluatorProvider.Get(evaluator).Evaluate(item, conditionalExpression.Conditions))
        {
            result = evaluator.Evaluate(item, context, conditionalExpression.ResultExpression);
        }
        else
        {
            result = evaluator.Evaluate(item, context, conditionalExpression.DefaultExpression);
        }
        return true;
    }
}
