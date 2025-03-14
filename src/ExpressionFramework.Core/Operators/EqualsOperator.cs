namespace ExpressionFramework.Core.Operators;

public partial record EqualsOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => Equal.Evaluate(leftValue, rightValue, StringComparison.CurrentCultureIgnoreCase);
}
