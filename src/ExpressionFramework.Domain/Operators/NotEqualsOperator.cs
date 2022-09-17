namespace ExpressionFramework.Domain.Operators;

public partial record NotEqualsOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(!EqualsOperator.IsValid(leftValue, rightValue));
}

