namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(ElementAtOrDefaultExpression))]
public partial record ElementAtOrDefaultExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            Expression,
            null,
            results => IndexExpression
                .EvaluateTyped<int>(context, "IndexExpression did not return an integer")
                .Transform(indexResult => indexResult.IsSuccessful()
                        ? indexResult.Value.Transform(index => results.Count() >= index
                            ? Result<object?>.Success(results.ElementAt(index))
                            : EnumerableExpression.GetDefaultValue(DefaultExpression, context))
                        : Result<object?>.FromExistingResult(indexResult))
        );

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(ElementAtOrDefaultExpression),
            "Gets the value at the specified index from the (enumerable) expression",
            "Value of the item at the specified index of the enumerable, or the default value",
            "This will be returned in case the enumerable is not empty, and no error occurs",
            "Expression is not of type enumerable, Enumerable is empty, Index is outside the bounds of the array",
            "This status (or any other status not equal to Ok) will be returned in case the index evaluation returns something else than Ok",
            hasDefaultExpression: true,
            resultValueType: typeof(object)
        );

    public ElementAtOrDefaultExpression(IEnumerable expression, int indexExpression, object? defaultExpression = null) : this(new TypedConstantExpression<IEnumerable>(expression), new TypedConstantExpression<int>(indexExpression), defaultExpression == null ? null : new ConstantExpression(defaultExpression)) { }
    public ElementAtOrDefaultExpression(Func<object?, IEnumerable> expression, Func<object?, int> indexExpression, Func<object?, object?>? defaultExpression = null) : this(new TypedDelegateExpression<IEnumerable>(expression), new TypedDelegateExpression<int>(indexExpression), defaultExpression == null ? null : new DelegateExpression(defaultExpression)) { }
}

public partial record ElementAtOrDefaultExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
