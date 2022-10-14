namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Filters an enumerable context value using a predicate")]
[ExpressionContextType(typeof(IEnumerable))]
[ExpressionContextDescription("The enumerable value to filter")]
[ExpressionContextRequired(true)]
[ParameterDescription(nameof(PredicateExpression), "Predicate to apply to each value. Return value must be a boolean value, so we can filter on it")]
[ParameterRequired(nameof(PredicateExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with items that satisfy the predicate", "This result will be returned when the context is enumerble, and the predicate returns a boolean value")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Context must be of type IEnumerable, Predicate did not return a boolean value")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case predicate evaluation fails")]
public partial record WhereExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is IEnumerable e
            ? EnumerableExpression.GetResultFromEnumerable(e, e => e
                .Select(x => new { Item = x, Result = PredicateExpression.Evaluate(x).TryCast<bool>("PredicateExpression did not return a boolean value") })
                .Where(x => !x.Result.IsSuccessful() || x.Result.Value)
                .Select(x => x.Result.IsSuccessful()
                    ? Result<object?>.Success(x.Item)
                    : Result<object?>.FromExistingResult(x.Result)))
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
        foreach (var itemResult in e.OfType<object>().Select(x => PredicateExpression.Evaluate(x)))
        {
            if (itemResult.IsSuccessful() && itemResult.Value is not bool)
            {
                yield return new ValidationResult($"PredicateExpression did not return a boolean value on item {index}");
            }

            if (itemResult.Status == ResultStatus.Invalid)
            {
                yield return new ValidationResult($"PredicateExpression returned an invalid result on item {index}. Error message: {itemResult.ErrorMessage}");
            }

            index++;
        }
    }
}

