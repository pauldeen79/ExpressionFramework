namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Filters an enumerable context value using a predicate")]
[ExpressionContextType(typeof(IEnumerable))]
[ExpressionContextDescription("The enumerable value to filter")]
[ExpressionContextRequired(true)]
[ParameterDescription(nameof(Predicate), "Predicate to apply to each value. Return value must be a boolean value, so we can filter on it")]
[ParameterRequired(nameof(Predicate), true)]
[ReturnValue(ResultStatus.Ok, "Enumerable with items that satisfy the predicate", "This result will be returned when the context is enumerble, and the predicate returns a boolean value")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Context must be of type IEnumerable, Predicate did not return a boolean value")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case predicate evaluation fails")]
public partial record WhereExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is IEnumerable e
            ? EnumerableExpression.GetResultFromEnumerable(e, e => e
                .Select(x => new { Item = x, Result = GetResult(Predicate.Evaluate(x)) })
                .Where(x => !x.Result.IsSuccessful() || x.Result.Value.IsTrue())
                .Select(x => x.Result.IsSuccessful() ? Result<object?>.Success(x.Item) : x.Result))
            : Result<object?>.Invalid("Context must be of type IEnumerable");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
    {
        if (context == null)
        {
            yield return new ValidationResult("Context cannot be empty");
            yield break;
        }

        if (context is not IEnumerable e)
        {
            yield return new ValidationResult("Context must be of type IEnumerable");
            yield break;
        }

        var index = 0;
        foreach (var itemResult in e.OfType<object>().Select(x => Predicate.Evaluate(x)))
        {
            if (itemResult.IsSuccessful() && itemResult.Value is not bool)
            {
                yield return new ValidationResult($"Predicate dit not return a boolean value on item {index}");
            }

            if (itemResult.Status == ResultStatus.Invalid)
            {
                yield return new ValidationResult($"Predicate returned an invalid result on item {index}. Error message: {itemResult.ErrorMessage}");
            }

            index++;
        }
    }

    private Result<object?> GetResult(Result<object?> itemResult)
    {
        if (!itemResult.IsSuccessful())
        {
            return itemResult;
        }

        if (itemResult.Value is not bool)
        {
            return Result<object?>.Invalid("Predicate did not return a boolean value");
        }

        return itemResult;
    }
}

