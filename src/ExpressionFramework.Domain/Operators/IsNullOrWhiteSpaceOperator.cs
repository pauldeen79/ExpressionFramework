namespace ExpressionFramework.Domain.Operators;

public partial record IsNullOrWhiteSpaceOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(leftValue == null || leftValue.ToString().Trim() == string.Empty);
}
