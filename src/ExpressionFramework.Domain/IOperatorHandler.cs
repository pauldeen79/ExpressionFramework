namespace ExpressionFramework.Domain;

public interface IOperatorHandler
{
    Result<bool> Handle(Operator @operator, object? leftValue, object? rightValue);
}
