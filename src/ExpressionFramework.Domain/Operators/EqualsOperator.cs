namespace ExpressionFramework.Domain.Operators;

public partial record EqualsOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(IsValid(leftValue, rightValue));

    internal static bool IsValid(object? leftValue, object? rightValue)
    {
        if (leftValue == null && rightValue == null)
        {
            return true;
        }

        if (leftValue == null || rightValue == null)
        {
            return false;
        }

        if (leftValue is string leftString
            && rightValue is string rightString
            && leftString.Equals(rightString, StringComparison.CurrentCultureIgnoreCase))
        {
            return true;
        }

        return leftValue.Equals(rightValue);
    }
}
