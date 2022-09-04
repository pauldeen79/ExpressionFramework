namespace ExpressionFramework.Domain.OperatorHandlers;

public class EqualsOperatorHandler : OperatorHandlerBase<EqualsOperator>
{
    protected override bool Evaluate(object? leftValue, object? rightValue)
        => (leftValue == null && rightValue == null)
        || (leftValue is string leftString && rightValue is string rightString && leftString.Equals(rightString, StringComparison.OrdinalIgnoreCase))
        || (leftValue != null && rightValue != null && leftValue.Equals(rightValue));
}
