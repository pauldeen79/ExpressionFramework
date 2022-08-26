namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class ConditionalExpressionEvaluatorHandler : IExpressionEvaluatorHandler
{
    private readonly IConditionEvaluatorProvider _conditionEvaluatorProvider;

    public ConditionalExpressionEvaluatorHandler(IConditionEvaluatorProvider conditionEvaluatorProvider)
        => _conditionEvaluatorProvider = conditionEvaluatorProvider;

    public Result<object?> Handle(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
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
