namespace ExpressionFramework.Domain.Operators;

public partial record IsNotNullOrEmptyOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(!(leftValue == null || leftValue.ToString() == string.Empty));
}
