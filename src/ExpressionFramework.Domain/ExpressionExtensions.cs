namespace ExpressionFramework.Domain;

public static class ExpressionExtensions
{
    public static Result<object?> EvaluateWithNullCheck(this Expression instance, object? context, string? errorMessage = null)
        => instance.Evaluate(context).Transform(result => result.IsSuccessful() && result.Value is null
            ? Result.Invalid<object?>(errorMessage.WhenNullOrEmpty("Expression cannot be empty"))
            : result);

    public static Result<T> EvaluateTyped<T>(this Expression instance, object? context = null, string? errorMessage = null)
        => instance is ITypedExpression<T> typedExpression
            ? typedExpression.EvaluateTyped(context)
            : instance.Evaluate(context).TryCast<T>(errorMessage);

    public static Result<T> EvaluateTypedWithTypeCheck<T>(this ITypedExpression<T> instance, object? context = null, string? errorMessage = null)
        => instance.EvaluateTyped(context).Transform(result => result.IsSuccessful() && result.Value is T t
            ? Result.FromExistingResult(result, t) // use FromExistingResult because status might be Ok, Continue or another successful status
            : result.Either(
                error => error,
                _ => Result.Invalid<T>(CreateInvalidTypeErrorMessage<T>(errorMessage))));

    public static string CreateInvalidTypeErrorMessage<T>(string? errorMessage = null)
        => errorMessage.WhenNullOrEmpty(() => $"Expression is not of type {GetTypeName(typeof(T))}");

    private static string GetTypeName(Type type)
    {
        if (type == typeof(string))
        {
            return "string";
        }

        if (typeof(IEnumerable).IsAssignableFrom(type))
        {
            return "enumerable";
        }

        return type.FullName;
    }
}
