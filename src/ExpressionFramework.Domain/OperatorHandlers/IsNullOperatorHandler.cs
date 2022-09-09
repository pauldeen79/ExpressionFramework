namespace ExpressionFramework.Domain.OperatorHandlers;

public class IsNullOperatorHandler : OperatorHandlerBase<IsNullOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
        => leftValue == null;
}
