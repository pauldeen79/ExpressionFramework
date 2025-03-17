namespace ExpressionFramework.Core.Operators;

public partial record IsNullOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => Result.Success(leftValue is null);
}
