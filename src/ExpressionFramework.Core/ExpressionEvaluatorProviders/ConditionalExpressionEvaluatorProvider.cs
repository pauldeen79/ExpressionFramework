namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class ConditionalExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    private readonly IConditionEvaluatorProvider _conditionEvaluatorProvider;

    public ConditionalExpressionEvaluatorProvider(IConditionEvaluatorProvider conditionEvaluatorProvider)
        => _conditionEvaluatorProvider = conditionEvaluatorProvider;

    public Result<object?> Evaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is not IConditionalExpression conditionalExpression)
        {
            return Result<object?>.NotSupported();
        }

        if (_conditionEvaluatorProvider.Get(evaluator).Evaluate(item, conditionalExpression.Conditions))
        {
            return evaluator.Evaluate(item, context, conditionalExpression.ResultExpression);
        }
        else
        {
            return evaluator.Evaluate(item, context, conditionalExpression.DefaultExpression);
        }
    }
}
