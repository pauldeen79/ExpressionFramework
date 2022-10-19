namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(ElementAtExpression))]
public partial record ElementAtExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetRequiredScalarValue
        (
            context,
            null,
            results => IndexExpression
                .Evaluate(context)
                .TryCast<int>("IndexExpression did not return an integer")
                .Transform(indexResult => Result<object?>.Success(results.ElementAt(indexResult.Value))),
            selectorDelegate: items =>
                IndexExpression
                    .Evaluate(context)
                    .TryCast<int>("IndexExpression did not return an integer")
                    .Transform(indexResult => indexResult.IsSuccessful()
                        ? indexResult.Value.Transform(index => items.Count() >= index
                            ? Result<IEnumerable<object?>>.Success(items)
                            : Result<IEnumerable<object?>>.Invalid("Index is outside the bounds of the array"))
                        : Result<IEnumerable<object?>>.FromExistingResult(indexResult))
        );

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context, () => IntExpression.ValidateParameter(context, IndexExpression, nameof(IndexExpression)));

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(ElementAtExpression),
            "Gets the value at the specified index from the (enumerable) context value",
            "Value of the item at the specified index of the enumerable",
            "This will be returned in case the enumerable is not empty, and no error occurs",
            "Context is not of type enumerable, Enumerable is empty, Index is outside the bounds of the array",
            "This status (or any other status not equal to Ok) will be returned in case the index evaluation returns something else than Ok",
            false
        );
}
