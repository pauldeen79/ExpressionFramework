namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Sorts items from an enumerable context value using sort expressions")]
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
    public override Result<object?> Evaluate(object? context) => Result<object?>.FromExistingResult(EvaluateTyped(context), result => result);

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression.ToUntyped());

    public Result<IEnumerable<object?>> EvaluateTyped(object? context)
        => Expression.EvaluateTypedWithTypeCheck(context).Transform(result =>
            result.IsSuccessful()
                ? GetSortedEnumerable(context, result.Value!.OfType<object?>())
                : Result<IEnumerable<object?>>.FromExistingResult(result));

    private Result<IEnumerable<object?>> GetSortedEnumerable(object? context, IEnumerable<object?> e)
    {
        var sortOrdersResult = GetSortOrdersResult(context);
        if (!sortOrdersResult.IsSuccessful())
        {
            return Result<IEnumerable<object?>>.FromExistingResult(sortOrdersResult);
        }

        if (!sortOrdersResult.Value!.Any())
        {
            return Result<IEnumerable<object?>>.Invalid("SortOrderExpressions should have at least one item");
        }

        if (!e.Any())
        {
            return Result<IEnumerable<object?>>.Success(e);
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

        return Result<IEnumerable<object?>>.Success(e);
    }

    private Result<IEnumerable<SortOrder>> GetSortOrdersResult(object? context)
    {
        var result = SortOrderExpressions.EvaluateTypedWithTypeCheck(context, "SortOrderExpressions is not of type SortOrder");

        return !result.IsSuccessful()
            ? Result<IEnumerable<SortOrder>>.FromExistingResult(result)
            : Result<IEnumerable<SortOrder>>.Success(result.Value!);
    }
}
