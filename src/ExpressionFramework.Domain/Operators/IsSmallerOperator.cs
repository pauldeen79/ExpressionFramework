namespace ExpressionFramework.Domain.Operators;

public partial record IsSmallerOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(leftValue != null
        && rightValue != null
        && leftValue is IComparable c
        && c.CompareTo(rightValue) < 0);
}
