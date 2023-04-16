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
        var enumerableResult = Expression.EvaluateTyped<IEnumerable>(context, "Expression is not of type enumerable");
        if (!enumerableResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(enumerableResult);
        }

        var keysResult = EnumerableExpression.GetTypedResultFromEnumerable(new ConstantExpression(enumerableResult.Value!), context, e => e.Select(x => KeySelectorExpression.Evaluate(x)).Distinct());
        if (!keysResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(keysResult);
        }

        return Result<object?>.Success(keysResult.Value.Select(x => new Grouping<object?, object?>(x, enumerableResult.Value!.OfType<object?>().Where(y => ItemSatisfiesKey(y, x)))));
    }

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    private bool ItemSatisfiesKey(object? item, object? key)
    {
        var val = KeySelectorExpression.Evaluate(item).Value;
        return (val is null && key is null) || (val != null && val.Equals(key));
    }

    [ExcludeFromCodeCoverage]
    private sealed class Grouping<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
    {
        public Grouping(TKey key, IEnumerable<TElement> collection)
            : base(collection) => Key = key;
        public TKey Key { get; }
    }
}

public partial record GroupByExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
