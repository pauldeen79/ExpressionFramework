namespace ExpressionFramework.Domain.OperatorHandlers;

public class EqualsOperatorHandler : OperatorHandlerBase<EqualsOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
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
