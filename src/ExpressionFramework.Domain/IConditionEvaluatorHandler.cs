namespace ExpressionFramework.Domain;

public interface IConditionEvaluatorHandler
{
    Result<bool> Handle(Operator @operator, object? leftValue, object? rightValue);
}
