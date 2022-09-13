namespace ExpressionFramework.Domain.Operators;

public partial record IsNullOrEmptyOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(leftValue == null || leftValue.ToString() == string.Empty);
}
