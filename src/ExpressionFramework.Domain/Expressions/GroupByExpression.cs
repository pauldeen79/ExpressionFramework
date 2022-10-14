namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Groups items from an enumerable context value using a key selector expression")]
[ExpressionContextType(typeof(IEnumerable))]
[ExpressionContextDescription("The enumerable value to group elements for")]
[ExpressionContextRequired(true)]
[ParameterDescription(nameof(KeySelectorExpression), "Expression to use on each item to select the key")]
[ParameterRequired(nameof(KeySelectorExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with grouped items (IGrouping<object, object>)", "This result will be returned when the context is enumerble")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Context must be of type IEnumerable")]
public partial record GroupByExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        if (context is not IEnumerable e)
        {
            return Result<object?>.Invalid("Context must be of type IEnumerable");
        }

        var keysResult = EnumerableExpression.GetTypedResultFromEnumerable(e, e => e.Select(x => KeySelectorExpression.Evaluate(x)).Distinct());
        if (!keysResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(keysResult);
        }

        return Result<object?>.Success(keysResult.Value.Select(x => new Grouping<object?, object?>(x, e.OfType<object?>().Where(y => ItemSatisfiesKey(y, x)))));
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
        foreach (var itemResult in e.OfType<object>().Select(x => KeySelectorExpression.Evaluate(x)))
        {
            if (itemResult.Status == ResultStatus.Invalid)
            {
                yield return new ValidationResult($"KeySelectorExpression returned an invalid result on item {index}. Error message: {itemResult.ErrorMessage}");
            }

            index++;
        }
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

