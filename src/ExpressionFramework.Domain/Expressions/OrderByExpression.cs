namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Sorts items from an enumerable context value using sort expressions")]
[ExpressionContextType(typeof(IEnumerable))]
[ExpressionContextDescription("The enumerable value to transform elements for")]
[ExpressionContextRequired(true)]
[ParameterDescription(nameof(SortOrders), "Sort orders to use")]
[ParameterRequired(nameof(SortOrder), true)]
[ReturnValue(ResultStatus.Ok, "Enumerable with sorted items", "This result will be returned when the context is enumerble")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Context must be of type IEnumerable, SortOrders should have at least one sort order")]
public partial record OrderByExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is IEnumerable e
            ? GetSortedEnumerable(e.OfType<object?>())
            : Result<object?>.Invalid("Context must be of type IEnumerable");

    private Result<object?> GetSortedEnumerable(IEnumerable<object?> e)
    {
        if (!SortOrders.Any())
        {
            return Result<object?>.Invalid("SortOrders should have at least one item");
        }

        if (!e.Any())
        {
            return Result<object?>.Success(e);
        }

        var first = true;
        IOrderedEnumerable<object?>? orderedEnumerable = null;
        foreach (var sortOrder in SortOrders)
        {
            var sortExpressionEvaluated = EnumerableExpression.GetResultFromEnumerable(e, x => x.Select(y => sortOrder.SortExpression.Evaluate(y)));
            if (!sortExpressionEvaluated.IsSuccessful())
            {
                return sortExpressionEvaluated;
            }
            if (first)
            {
                first = false;
                orderedEnumerable = sortOrder.Direction == SortOrderDirection.Ascending
                    ? e.OrderBy(x => sortOrder.SortExpression.Evaluate(x).Value)
                    : e.OrderByDescending(x => sortOrder.SortExpression.Evaluate(x).Value);
                e = orderedEnumerable;
            }
            else
            {
                orderedEnumerable = sortOrder.Direction == SortOrderDirection.Ascending
                    ? orderedEnumerable.ThenBy(x => sortOrder.SortExpression.Evaluate(x).Value)
                    : orderedEnumerable.ThenByDescending(x => sortOrder.SortExpression.Evaluate(x).Value);
                e = orderedEnumerable;
            }
        }

        return Result<object?>.Success(e);
    }

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
        foreach (var sortOrder in SortOrders)
        {
            foreach (var itemResult in e.OfType<object>().Select(x => sortOrder.SortExpression.Evaluate(x)))
            {
                if (itemResult.Status == ResultStatus.Invalid)
                {
                    yield return new ValidationResult($"SortExpression returned an invalid result on item {index}. Error message: {itemResult.ErrorMessage}");
                }

                index++;
            }
        }
    }
}

