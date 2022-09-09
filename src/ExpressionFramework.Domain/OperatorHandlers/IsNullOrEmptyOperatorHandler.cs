namespace ExpressionFramework.Domain.OperatorHandlers;

public class IsNullOrEmptyOperatorHandler : OperatorHandlerBase<IsNullOrEmptyOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
        => leftValue == null || leftValue.ToString() == string.Empty;
}
