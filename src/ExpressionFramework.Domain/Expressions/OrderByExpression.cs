namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Sorts items from an enumerable expression using sort expressions")]
[ContextDescription("The enumerable value to transform elements for")]
[ContextType(typeof(IEnumerable))]
[ParameterDescription(nameof(SortOrderExpressions), "Sort orders to use")]
[ParameterRequired(nameof(SortOrderExpressions), true)]
[ParameterDescription(nameof(Expression), "Enumerable to get ordered items for")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(IEnumerable))]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with sorted items", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression is not of type enumerable, SortOrders should have at least one sort order")]
public partial record OrderByExpression
{
    public override Result<object?> Evaluate(object? context) => Result.FromExistingResult<IEnumerable<object?>, object?>(EvaluateTyped(context), result => result);

    public Result<IEnumerable<object?>> EvaluateTyped(object? context)
        => Expression.EvaluateTyped(context).Transform(result =>
            result.IsSuccessful()
                ? result.Transform(x => x.Value is null
                    ? Result.Invalid<IEnumerable<object?>>("Expression is not of type enumerable")
                    : GetSortedEnumerable(context, result.Value!.OfType<object?>()))
                : Result.FromExistingResult<IEnumerable<object?>>(result));

    private Result<IEnumerable<object?>> GetSortedEnumerable(object? context, IEnumerable<object?> e)
    {
        var sortOrdersResult = GetSortOrdersResult(context);
        if (!sortOrdersResult.IsSuccessful())
        {
            return Result.FromExistingResult<IEnumerable<object?>>(sortOrdersResult);
        }

        if (!sortOrdersResult.Value!.Any())
        {
            return Result.Invalid<IEnumerable<object?>>("SortOrderExpressions should have at least one item");
        }

        if (!e.Any())
        {
            return Result.Success(e);
        }

        var first = true;
        IOrderedEnumerable<object?>? orderedEnumerable = null;
        foreach (var sortOrder in sortOrdersResult.Value!)
        {
            var sortExpressionEvaluated = EnumerableExpression.GetTypedResultFromEnumerable(new TypedConstantExpression<IEnumerable>(e), context, x => x.Select(y => sortOrder.SortExpression.Evaluate(y)));
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

        return Result.Success(e);
    }

    private Result<IEnumerable<SortOrder>> GetSortOrdersResult(object? context)
    {
        var items = new List<SortOrder>();

        var index = 0;
        foreach (var sortOrderResult in SortOrderExpressions.Select(x => x.EvaluateTypedWithTypeCheck(context, $"SortOrderExpressions item with index {index} is not of type SortOrder")))
        {
            if (!sortOrderResult.IsSuccessful())
            {
                return Result.FromExistingResult<IEnumerable<SortOrder>>(sortOrderResult);
            }

            items.Add(sortOrderResult.Value!);
            index++;
        }

        return Result.Success<IEnumerable<SortOrder>>(items);
    }
}
