namespace ExpressionFramework.Domain.OperatorHandlers;

public class NotEndsWithOperatorHandler : OperatorHandlerBase<NotEndsWithOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
        => leftValue != null && !EndsWithOperatorHandler.IsValid(leftValue, rightValue);
}

