namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(FirstExpression))]
public partial record FirstExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetRequiredScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            results => Result<object?>.Success(results.First()),
            results => Result<object?>.Success(results.First(x => x.Result.Value).Item)
        );

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(FirstExpression),
            "Gets the first value from the (enumerable) expression, optionally using a predicate to select an item",
            "Value of the first item of the enumerable that conforms to the predicate",
            "This will be returned in case the enumerable is not empty, and no error occurs",
            "Expression is not of type enumerable, Enumerable is empty, Predicate did not return a boolean value, None of the items conform to the supplied predicate",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            hasDefaultExpression: false,
            resultValueType: typeof(object)
        );
}

