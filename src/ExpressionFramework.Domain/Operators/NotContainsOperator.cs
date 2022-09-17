namespace ExpressionFramework.Domain.Operators;

public partial record NotContainsOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(leftValue != null && !ContainsOperator.IsValid(leftValue, rightValue));
}
