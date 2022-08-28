namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class SwitchEpressionEvaluationHandler : IExpressionEvaluatorHandler
{
    private readonly IConditionEvaluatorProvider _conditionEvaluatorProvider;

    public SwitchEpressionEvaluationHandler(IConditionEvaluatorProvider conditionEvaluatorProvider)
        => _conditionEvaluatorProvider = conditionEvaluatorProvider;

    public Result<object?> Handle(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is not ISwitchExpression switchExpression)
        {
            return Result<object?>.NotSupported();
        }

        var conditionEvaluator = _conditionEvaluatorProvider.Get(evaluator);
        foreach (var @case in switchExpression.Cases)
        {
            var caseResult = conditionEvaluator.Evaluate(item, @case.Conditions);
            if (caseResult)
            {
                return evaluator.Evaluate(item, context, @case.Expression);
            }
        }

        return evaluator.Evaluate(item, context, switchExpression.DefaultExpression);
    }
}
