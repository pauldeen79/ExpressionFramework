namespace ExpressionFramework.Core.Operators;

public partial record NotEqualsOperator
{
    public override CrossCutting.Common.Results.Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => NotEqual.Evaluate(leftValue, rightValue, stringComparison);
}
