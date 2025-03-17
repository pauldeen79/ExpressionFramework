namespace ExpressionFramework.Core.Operators;

public partial record IsNullOrEmptyOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => Result.Success(leftValue is null || leftValue.ToString().Length == 0);
}
