namespace ExpressionFramework.Domain.Operators;

public partial record NotStartsWithOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(leftValue != null && !StartsWithOperator.IsValid(leftValue, rightValue));
}
