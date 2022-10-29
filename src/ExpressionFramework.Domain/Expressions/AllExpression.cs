namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(AllExpression))]
public partial record AllExpression : ITypedExpression<bool>
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            null!, // you can't get here... predicate is always checked
            results => Result<object?>.Success(results.All(x => x.Result.Value)),
            predicateIsRequired: true
        );

    public Result<bool> EvaluateTyped(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            Expression,
            PredicateExpression,
            null!, // you can't get here... predicate is always checked
            results => Result<bool>.Success(results.All(x => x.Result.Value)),
            predicateIsRequired: true
        );

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(AllExpression),
            "Returns an indicator whether all items from the (enumerable) expression conform to the predicate, optionally using a predicate",
            "True when all items in the enumerable conform to the predicate, otherwise false",
            "This will be returned in case no error occurs",
            "Expression is not of type enumerable, Predicate did not return a boolean value",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            hasDefaultExpression: false,
            predicateIsRequired: true
        );
}

