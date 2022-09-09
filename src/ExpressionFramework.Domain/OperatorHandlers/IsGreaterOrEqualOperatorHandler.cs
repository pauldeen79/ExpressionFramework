namespace ExpressionFramework.Domain.OperatorHandlers;

public class IsGreaterOrEqualOperatorHandler : OperatorHandlerBase<IsGreaterOrEqualOperator>
{
    protected override bool Handle(System.Object? leftValue, System.Object? rightValue)
        => leftValue != null
        && rightValue != null
        && leftValue is IComparable c
        && c.CompareTo(rightValue) >= 0;
}
