namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Groups items from an enumerable context value using a key selector expression")]
[ContextDescription("Value to use as context in the expression")]
[ContextType(typeof(IEnumerable))]
[ParameterDescription(nameof(KeySelectorExpression), "Expression to use on each item to select the key")]
[ParameterRequired(nameof(KeySelectorExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with grouped items (IGrouping<object, object>)", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Context is not of type enumerable")]
public partial record GroupByExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var enumerableResult = Expression.EvaluateTyped(context);
        if (!enumerableResult.IsSuccessful())
        {
            return Result.FromExistingResult<object?>(enumerableResult);
        }

        var keysResult = EnumerableExpression.GetTypedResultFromEnumerable(new TypedConstantExpression<IEnumerable>(enumerableResult.Value!), context, e => e.Select(x => KeySelectorExpression.Evaluate(x)).Distinct());
        if (!keysResult.IsSuccessful())
        {
            return Result.FromExistingResult<object?>(keysResult);
        }

        return Result.Success<object?>(keysResult.Value.Select(x => new Grouping<object?, object?>(x, enumerableResult.Value!.OfType<object?>().Where(y => ItemSatisfiesKey(y, x)))));
    }

    private bool ItemSatisfiesKey(object? item, object? key)
    {
        var val = KeySelectorExpression.Evaluate(item).Value;
        return (val is null && key is null) || (val != null && val.Equals(key));
    }

    [ExcludeFromCodeCoverage]
    private sealed class Grouping<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
    {
        public Grouping(TKey key, IEnumerable<TElement> collection) : base(collection) => Key = key;
        public TKey Key { get; }
    }
}
