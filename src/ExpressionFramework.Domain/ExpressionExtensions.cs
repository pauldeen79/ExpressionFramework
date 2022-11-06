namespace ExpressionFramework.Domain;

public static class ExpressionExtensions
{
    public static Result<T> EvaluateTyped<T>(this Expression instance, object? context = null, string? errorMessage = null)
        => instance is ITypedExpression<T> typedExpression
            ? typedExpression.EvaluateTyped(context)
            : instance.Evaluate(context).TryCast<T>(errorMessage);
}
