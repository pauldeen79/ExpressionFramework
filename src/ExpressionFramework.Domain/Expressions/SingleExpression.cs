namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(SingleExpression))]
public partial record SingleExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetRequiredScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            results => Result<object?>.Success(results.Single()),
            results => Result<object?>.Success(results.Single(x => x.Result.Value).Item),
            items => items.Count(x => PredicateExpression == null || PredicateExpression.EvaluateTyped<bool>(x, "Predicate did not return a boolean value").Value) > 1
                ? Result<IEnumerable<object?>>.Invalid("Sequence contains more than one element")
                : Result<IEnumerable<object?>>.Success(items)
        );

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(SingleExpression),
            "Gets a single value from the (enumerable) expression, optionally using a predicate to select an item",
            "Value of the single item of the enumerable that conforms to the predicate",
            "This will be returned in case the enumerable contains a single element, and no error occurs",
            "Expression is not of type enumerable, Enumerable is empty, Predicate did not return a boolean value, None of the items conform to the supplied predicate, Sequence contains one than one element",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            hasDefaultExpression: false,
            resultValueType: typeof(object)
        );
}

public partial record SingleExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
