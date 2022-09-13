namespace ExpressionFramework.Domain.Operators;

public partial record IsNotNullOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(leftValue != null);
}
