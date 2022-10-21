namespace ExpressionFramework.Domain;

public static class EnumerableOfExpressionExtensions
{
    public static Result<object?>[] EvaluateUntilFirstError(this IEnumerable<Expression> expressions, object? context)
        => expressions
            .Select(x => x.Evaluate(context))
            .TakeWhileWithFirstNonMatching(x => x.IsSuccessful())
            .ToArray();

    public static Result<T>[] EvaluateTypedUntilFirstError<T>(this IEnumerable<Expression> expressions, object? context, string? errorMessage = null)
        => expressions
            .Select(x => x.EvaluateTyped<T>(context, errorMessage))
            .TakeWhileWithFirstNonMatching(x => x.IsSuccessful())
            .ToArray();
}
