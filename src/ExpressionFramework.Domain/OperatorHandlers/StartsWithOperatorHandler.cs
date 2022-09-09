namespace ExpressionFramework.Domain.OperatorHandlers;

public class StartsWithOperatorHandler : OperatorHandlerBase<StartsWithOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
        => IsValid(leftValue, rightValue);

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

        return leftValue.ToString().StartsWith(rightValueString, StringComparison.CurrentCultureIgnoreCase);
    }
}

