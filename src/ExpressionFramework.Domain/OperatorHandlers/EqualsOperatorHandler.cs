namespace ExpressionFramework.Domain.OperatorHandlers;

public class EqualsOperatorHandler : OperatorHandlerBase<EqualsOperator>
{
    protected override bool Evaluate(object? leftValue, object? rightValue)
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
            && leftString.Equals(rightString, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return leftValue.Equals(rightValue);
    }
}
