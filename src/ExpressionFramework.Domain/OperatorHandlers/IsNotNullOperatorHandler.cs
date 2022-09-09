namespace ExpressionFramework.Domain.OperatorHandlers;

public class IsNotNullOperatorHandler : OperatorHandlerBase<IsNotNullOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
        => leftValue != null;
}
