namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(AnyExpression))]
public partial record AnyExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            results => Result.Success<object?>(results.Any()),
            results => Result.Success<object?>(results.Any(x => x.Result.Value))
        );

    public Result<bool> EvaluateTyped(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            results => Result.Success<bool>(results.Any()),
            results => Result.Success<bool>(results.Any(x => x.Result.Value))
        );

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(AnyExpression),
            "Returns an indicator whether any item from the (enumerable) expression conform to the predicate, optionally using a predicate",
            "True when any item in the enumerable conform to the predicate, otherwise false",
            "This will be returned in case no error occurs",
            "Expression is not of type enumerable, Predicate did not return a boolean value",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            hasDefaultExpression: false,
            resultValueType: typeof(bool)
        );
}
