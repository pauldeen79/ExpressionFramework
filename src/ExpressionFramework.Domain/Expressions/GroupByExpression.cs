namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Groups items from an enumerable context value using a key selector expression")]
[ContextType(typeof(IEnumerable))]
[ContextDescription("The enumerable value to group elements for")]
[ContextRequired(true)]
[ParameterDescription(nameof(KeySelectorExpression), "Expression to use on each item to select the key")]
[ParameterRequired(nameof(KeySelectorExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with grouped items (IGrouping<object, object>)", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Context is not of type enumerable")]
public partial record GroupByExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        if (context is not IEnumerable e)
        {
            return Result<object?>.Invalid("Context is not of type enumerable");
        }

        var keysResult = EnumerableExpression.GetTypedResultFromEnumerable(e, e => e.Select(x => KeySelectorExpression.Evaluate(x)).Distinct());
        if (!keysResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(keysResult);
        }

        return Result<object?>.Success(keysResult.Value.Select(x => new Grouping<object?, object?>(x, e.OfType<object?>().Where(y => ItemSatisfiesKey(y, x)))));
    }

    private bool ItemSatisfiesKey(object? item, object? key)
    {
        var val = KeySelectorExpression.Evaluate(item).Value;
        return (val == null && key == null) || (val != null && val.Equals(key));
    }

    [ExcludeFromCodeCoverage]
    private sealed class Grouping<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
    {
        public Grouping(TKey key, IEnumerable<TElement> collection)
            : base(collection) => Key = key;
        public TKey Key { get; }
    }
}

