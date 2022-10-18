namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(ElementAtOrDefaultExpression))]
public partial record ElementAtOrDefaultExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetScalarValueWithDefault
        (
            context,
            null,
            results => IndexExpression
                .Evaluate(context)
                .TryCast<int>("IndexExpression did not return an integer")
                .Transform(indexResult => indexResult.IsSuccessful()
                        ? indexResult.Value.Transform(index => results.Count() >= index
                            ? Result<object?>.Success(results.ElementAt(index))
                            : EnumerableExpression.GetDefaultValue(DefaultExpression, context))
                        : Result<object?>.FromExistingResult(indexResult))
        );

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context, () => IntExpression.ValidateParameter(context, IndexExpression, nameof(IndexExpression)));

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(ElementAtOrDefaultExpression),
            "Gets the value at the specified index from the (enumerable) context value",
            "Value of the item at the specified index of the enumerable, or the default value",
            "This will be returned in case the enumerable is not empty, and no error occurs",
            "Context is not of type enumerable, Enumerable is empty, Index is outside the bounds of the array",
            "This status (or any other status not equal to Ok) will be returned in case the index evaluation returns something else than Ok",
            true
        );
}

