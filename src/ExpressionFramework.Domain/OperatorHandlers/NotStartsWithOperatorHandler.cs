namespace ExpressionFramework.Domain.OperatorHandlers;

public class NotStartsWithOperatorHandler : OperatorHandlerBase<NotStartsWithOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
        => leftValue != null && !StartsWithOperatorHandler.IsValid(leftValue, rightValue);
}
