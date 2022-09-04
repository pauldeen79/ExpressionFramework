namespace ExpressionFramework.Domain;

public interface IConditionEvaluatorProvider
{
    IConditionEvaluator Get(IExpressionEvaluator expressionEvaluator);
}
