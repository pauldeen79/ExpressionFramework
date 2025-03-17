namespace ExpressionFramework.Core.Operators;

public partial record IsGreaterOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => GreaterThan.Evaluate(leftValue, rightValue);
}
