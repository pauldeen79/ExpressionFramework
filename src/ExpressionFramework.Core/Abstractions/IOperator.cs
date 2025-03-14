namespace ExpressionFramework.Core.Abstractions;

public partial interface IOperator
{
    Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison);
}
