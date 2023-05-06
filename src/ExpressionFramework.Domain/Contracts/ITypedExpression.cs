namespace ExpressionFramework.Domain.Contracts;

public interface ITypedExpression<T> : IUntypedExpressionProvider
{
    Result<T> EvaluateTyped(object? context);
}
