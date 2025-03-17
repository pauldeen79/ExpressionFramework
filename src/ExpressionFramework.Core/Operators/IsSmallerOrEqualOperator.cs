namespace ExpressionFramework.Core.Operators;

public partial record IsSmallerOrEqualOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => SmallerOrEqualThan.Evaluate(leftValue, rightValue);
}
