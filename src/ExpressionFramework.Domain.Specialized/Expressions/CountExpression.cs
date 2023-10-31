namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(CountExpression))]
public partial record CountExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            results => Result.Success<object?>(results.Count()),
            results => Result.Success<object?>(results.Count(x => x.Result.Value))
        );

    public Result<int> EvaluateTyped(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            results => Result.Success<int>(results.Count()),
            results => Result.Success<int>(results.Count(x => x.Result.Value))
        );

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(CountExpression),
            "Gets the number of items from the (enumerable) expression, optionally using a predicate",
            "Number of items in the enumerable that conforms to the predicate",
            "This will be returned in case no error occurs",
            "Expression is not of type enumerable, Predicate did not return a boolean value",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            hasDefaultExpression: false,
            resultValueType: typeof(int)
        );
}
