namespace ExpressionFramework.Core.Operators;

public partial record IsSmallerOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => SmallerThan.Evaluate(leftValue, rightValue);
}
