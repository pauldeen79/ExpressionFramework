namespace ExpressionFramework.Core;

public partial record OperatorBase
{
    public abstract Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison);
}
