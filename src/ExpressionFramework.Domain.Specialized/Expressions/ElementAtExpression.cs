namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(ElementAtExpression))]
public partial record ElementAtExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetRequiredScalarValue
        (
            context,
            Expression,
            null,
            results => IndexExpression
                .EvaluateTyped(context)
                .Transform(indexResult => Result<object?>.Success(results.ElementAt(indexResult.Value))),
            selectorDelegate: items =>
                IndexExpression
                    .EvaluateTyped(context)
                    .Transform(indexResult => indexResult.IsSuccessful()
                        ? indexResult.Value.Transform(index => items.Count() >= index
                            ? Result<IEnumerable<object?>>.Success(items)
                            : Result<IEnumerable<object?>>.Invalid("Index is outside the bounds of the array"))
                        : Result<IEnumerable<object?>>.FromExistingResult(indexResult))
        );

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(ElementAtExpression),
            "Gets the value at the specified index from the (enumerable) expression",
            "Value of the item at the specified index of the enumerable",
            "This will be returned in case the enumerable is not empty, and no error occurs",
            "Expression is not of type enumerable, Enumerable is empty, Index is outside the bounds of the array",
            "This status (or any other status not equal to Ok) will be returned in case the index evaluation returns something else than Ok",
            hasDefaultExpression: false,
            resultValueType: typeof(object)
        );
}
