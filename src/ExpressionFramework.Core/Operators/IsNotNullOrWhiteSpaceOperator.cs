namespace ExpressionFramework.Core.Operators;

public partial record IsNotNullOrWhiteSpaceOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => Result.Success(!(leftValue is null || leftValue.ToString().Trim().Length == 0));
}
