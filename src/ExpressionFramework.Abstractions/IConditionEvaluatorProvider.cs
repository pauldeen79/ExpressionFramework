namespace ExpressionFramework.Abstractions;

public interface IConditionEvaluatorProvider
{
    IConditionEvaluator Get(IExpressionEvaluator expressionEvaluator);
}
