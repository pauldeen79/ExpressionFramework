namespace ExpressionFramework.Domain;

public interface ITypedExpression<T>
{
    Result<T> EvaluateTyped(object? context);
}
