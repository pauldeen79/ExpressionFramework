namespace ExpressionFramework.Domain.Operators;

public partial record NotEndsWithOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(leftValue != null && !EndsWithOperator.IsValid(leftValue, rightValue));
}

