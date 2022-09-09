namespace ExpressionFramework.Domain.OperatorHandlers;

public class NotContainsOperatorHandler : OperatorHandlerBase<NotContainsOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
        => leftValue != null && !ContainsOperatorHandler.IsValid(leftValue, rightValue);
}
