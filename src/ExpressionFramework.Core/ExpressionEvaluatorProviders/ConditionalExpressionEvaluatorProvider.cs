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

        if (_conditionEvaluatorProvider.Get(evaluator).Evaluate(item, conditionalExpression.Conditions)) //TODO: Review if we need item or context here...
        {
            result = evaluator.Evaluate(item, context, conditionalExpression.ResultExpression);
        }
        else
        {
            result = default; //TODO: Add default/false expression
        }
        //result = _conditionEvaluatorProvider.Get(evaluator).Evaluate(item, conditionalExpression.Conditions);
        return true;
    }
}
