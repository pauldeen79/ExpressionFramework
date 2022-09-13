namespace ExpressionFramework.Domain;

public abstract partial record Operator
{
    public abstract Result<bool> Evaluate(object? leftValue, object? rightValue);
}
