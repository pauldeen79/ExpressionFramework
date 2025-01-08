namespace ExpressionFramework.Domain;

public static class TypedExpressionExtensions
{
    public static Result<T> EvaluateTyped<T>(this ITypedExpression<T> instance)
        => instance.EvaluateTyped(null);
}
