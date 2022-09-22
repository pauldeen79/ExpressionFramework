namespace ExpressionFramework.Domain;

public static class EnumerableOfExpressionExtensions
{
    public static Result<object?>[] EvaluateUntilFirstError(this IEnumerable<Expression> expressions, object? context)
        => expressions
            .Select(x => x.Evaluate(context))
            .TakeWhileWithFirstNonMatching(x => x.IsSuccessful())
            .ToArray();
}
