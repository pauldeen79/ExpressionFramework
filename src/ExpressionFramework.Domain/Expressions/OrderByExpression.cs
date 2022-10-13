namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Sorts items from an enumerable context value using sort expressions")]
[ExpressionContextType(typeof(IEnumerable))]
[ExpressionContextDescription("The enumerable value to transform elements for")]
[ExpressionContextRequired(true)]
[ParameterDescription(nameof(SortOrderExpressions), "Sort orders to use")]
[ParameterRequired(nameof(SortOrderExpressions), true)]
[ReturnValue(ResultStatus.Ok, "Enumerable with sorted items", "This result will be returned when the context is enumerble")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Context must be of type IEnumerable, SortOrders should have at least one sort order")]
public partial record OrderByExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is IEnumerable e
            ? GetSortedEnumerable(context, e.OfType<object?>())
            : Result<object?>.Invalid("Context must be of type IEnumerable");

    private Result<object?> GetSortedEnumerable(object? context, IEnumerable<object?> e)
    {
        var sortOrdersResult = GetSortOrdersResult(context);
        if (!sortOrdersResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(sortOrdersResult);
        }

        if (!sortOrdersResult.Value!.Any())
        {
            return Result<object?>.Invalid("SortOrders should have at least one item");
        }

        if (!e.Any())
        {
            return Result<object?>.Success(e);
        }

        var first = true;
        IOrderedEnumerable<object?>? orderedEnumerable = null;
        foreach (var sortOrder in sortOrdersResult.Value!)
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

    private Result<IEnumerable<SortOrder>> GetSortOrdersResult(object? context)
    {
        var results = SortOrderExpressions.EvaluateUntilFirstError(context);
        return CastToSortOrders(results);
    }

    private Result<IEnumerable<SortOrder>> CastToSortOrders(IEnumerable<Result<object?>> results)
    {
        var items = new List<SortOrder>();

        var index = 0;
        foreach (var result in results)
        {
            if (!result.IsSuccessful())
            {
                return Result<IEnumerable<SortOrder>>.FromExistingResult(result);
            }

            if (result.Value is not SortOrder sortOrder)
            {
                return Result<IEnumerable<SortOrder>>.Invalid($"SortOrderExpressions item with index {index} is not of type SortOrder");
            }

            items.Add(sortOrder);
            index++;
        }

        return Result<IEnumerable<SortOrder>>.Success(items);
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

        var sortOrdersResult = GetSortOrdersResult(context);
        if (sortOrdersResult.Status == ResultStatus.Invalid)
        {
            yield return new ValidationResult($"SortOrderExpressions returned an invalid result. Error message: {sortOrdersResult.ErrorMessage}");
            yield break;
        }
        if (!sortOrdersResult.Value!.Any())
        {
            yield return new ValidationResult("SortOrders should have at least one item");
            yield break;
        }

        var index = 0;
        foreach (var sortOrder in sortOrdersResult.Value!)
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

    public OrderByExpression(IEnumerable<SortOrder> sortOrders) : this(sortOrders.Select(x => new ConstantExpression(x))) { }
}

