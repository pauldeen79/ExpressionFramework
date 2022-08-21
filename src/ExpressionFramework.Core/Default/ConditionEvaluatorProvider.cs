namespace ExpressionFramework.Core.Default;

public class ConditionEvaluatorProvider : IConditionEvaluatorProvider
{
    public IConditionEvaluator Get(IExpressionEvaluator expressionEvaluator)
        => new ConditionEvaluator(expressionEvaluator);
}
