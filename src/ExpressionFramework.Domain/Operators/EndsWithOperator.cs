namespace ExpressionFramework.Domain.Operators;

public partial record EndsWithOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(IsValid(leftValue, rightValue));

    internal static bool IsValid(object? leftValue, object? rightValue)
    {
        if (leftValue == null)
        {
            return false;
        }

        if (rightValue is not string rightValueString || string.IsNullOrEmpty(rightValueString))
        {
            return false;
        }

        return leftValue.ToString().EndsWith(rightValueString, StringComparison.CurrentCultureIgnoreCase);
    }
}

