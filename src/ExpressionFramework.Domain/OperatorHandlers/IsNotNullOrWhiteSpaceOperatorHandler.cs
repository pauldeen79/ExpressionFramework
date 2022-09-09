namespace ExpressionFramework.Domain.OperatorHandlers;

public class IsNotNullOrWhiteSpaceOperatorHandler : OperatorHandlerBase<IsNotNullOrWhiteSpaceOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
        => !(leftValue == null || leftValue.ToString().Trim() == string.Empty);
}
