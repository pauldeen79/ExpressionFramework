namespace ExpressionFramework.Domain.OperatorHandlers;

public class NotStartsWithOperatorHandler : OperatorHandlerBase<NotStartsWithOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
    {
        if (leftValue == null)
        {
            return true;
        }

        if (rightValue is not string rightValueString || string.IsNullOrEmpty(rightValueString))
        {
            return true;
        }

        return !leftValue.ToString().StartsWith(rightValueString, StringComparison.CurrentCultureIgnoreCase);
    }
}
