namespace ExpressionFramework.Core.Operators;

public partial record IsNotNullOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => Result.Success(leftValue is not null);
}
