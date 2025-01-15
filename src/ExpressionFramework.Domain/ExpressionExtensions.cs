namespace ExpressionFramework.Domain;

public static class ExpressionExtensions
{
    public static Result<object?> EvaluateWithNullCheck(this Expression instance, object? context, string? errorMessage = null)
        => instance.Evaluate(context).Transform(result => result.IsSuccessful() && result.Value is null
            ? Result.Invalid<object?>(errorMessage.WhenNullOrEmpty("Expression cannot be empty"))
            : result);

    public static Result<T> EvaluateTyped<T>(this Expression instance, object? context = null, string? errorMessage = null)
    {
        if (instance is ITypedExpression<T> typedExpression)
        {
            return typedExpression.EvaluateTyped(context);
        }

        var result = instance.Evaluate(context).TryCast<T>(errorMessage);

        if (result.IsSuccessful() && result.GetValue() is null)
        {
            //HACK: Null values now work differently with TryCast. We need a new method on Result to fix this... For now, do a work-around.
            return errorMessage is not null
                ? Result.Invalid<T>(errorMessage)
                : Result.Invalid<T>();
        }

        return result;
    }

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
