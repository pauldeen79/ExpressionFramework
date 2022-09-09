namespace ExpressionFramework.Domain.OperatorHandlers;

public class NotEqualsOperatorHandler : OperatorHandlerBase<NotEqualsOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
        => !EqualsOperatorHandler.IsValid(leftValue, rightValue);
}

