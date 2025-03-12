namespace ExpressionFramework.Core.Abstractions;

public interface IOperator
{
    Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison);
}
