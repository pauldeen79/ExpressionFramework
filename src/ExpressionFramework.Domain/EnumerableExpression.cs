namespace ExpressionFramework.Domain;

public static class EnumerableExpression
{
    public static Result<object?> GetResultFromEnumerable(IEnumerable e, Func<IEnumerable<object>, IEnumerable<Result<object?>>> @delegate)
    {
        var results = @delegate(e.OfType<object>()).TakeWhileWithFirstNonMatching(x => x.IsSuccessful()).ToArray();
        if (!results.Last().IsSuccessful())
        {
            return Result<object?>.FromExistingResult(results.Last());
        }

        return Result<object?>.Success(results.Select(x => x.Value));
    }
}
