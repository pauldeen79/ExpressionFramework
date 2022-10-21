namespace ExpressionFramework.Domain;

public static class ExpressionExtensions
{
    public static Result<T> EvaluateTyped<T>(this Expression instance, object? context, string? errorMessage = null)
    {
        if (instance is ITypedExpression<T> typedExpression)
        {
            return typedExpression.EvaluateTyped(context);
        }

        return instance.Evaluate(context).TryCast<T>(errorMessage);
    }
}
