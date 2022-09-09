namespace ExpressionFramework.Domain.OperatorHandlers;

public class IsNullOrWhiteSpaceOperatorHandler : OperatorHandlerBase<IsNullOrWhiteSpaceOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
        => leftValue == null || leftValue.ToString().Trim() == string.Empty;
}
