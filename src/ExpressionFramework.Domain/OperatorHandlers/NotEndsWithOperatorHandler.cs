namespace ExpressionFramework.Domain.OperatorHandlers;

public class NotEndsWithOperatorHandler : OperatorHandlerBase<NotEndsWithOperator>
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

        return !leftValue.ToString().EndsWith(rightValueString, StringComparison.CurrentCultureIgnoreCase);
    }
}

