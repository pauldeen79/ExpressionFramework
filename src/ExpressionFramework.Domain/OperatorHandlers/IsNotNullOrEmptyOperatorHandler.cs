namespace ExpressionFramework.Domain.OperatorHandlers;

public class IsNotNullOrEmptyOperatorHandler : OperatorHandlerBase<IsNotNullOrEmptyOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
        => !(leftValue == null || leftValue.ToString() == string.Empty);
}
