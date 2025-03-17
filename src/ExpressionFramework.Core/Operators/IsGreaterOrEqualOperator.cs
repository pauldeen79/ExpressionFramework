namespace ExpressionFramework.Core.Operators;

public partial record IsGreaterOrEqualOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => GreaterOrEqualThan.Evaluate(leftValue, rightValue);
}
