namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(SingleOrDefaultExpression))]
public partial record SingleOrDefaultExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            results => Result.Success<object?>(results.Single()),
            results => Result.Success<object?>(results.Single(x => x.Result.Value).Item),
            context => EnumerableExpression.GetDefaultValue(DefaultExpression, context),
            items => items.Count(x => PredicateExpression is null || PredicateExpression.EvaluateTyped(x).Value) > 1
                ? Result.Invalid<IEnumerable<object?>>("Sequence contains more than one element")
                : Result.Success<IEnumerable<object?>>(items)
        );

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(SingleOrDefaultExpression),
            "Gets a single value from the (enumerable) expression, optionally using a predicate to select an item",
            "Value of the single item of the enumerable that conforms to the predicate",
            "This will be returned in case the enumerable contains a single element, and no error occurs",
            "Expression is not of type enumerable, Predicate did not return a boolean value, Sequence contains one than one element",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            hasDefaultExpression: true,
            resultValueType: typeof(object)
        );
}
