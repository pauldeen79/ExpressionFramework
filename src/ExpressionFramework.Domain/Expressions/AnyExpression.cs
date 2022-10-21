namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(AnyExpression))]
public partial record AnyExpression : ITypedExpression<bool>
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            PredicateExpression,
            results => Result<object?>.Success(results.Any()),
            results => Result<object?>.Success(results.Any(x => x.Result.Value))
        );

    public Result<bool> EvaluateTyped(object? context)
        => EnumerableExpression.GetOptionalScalarValue
        (
            context,
            PredicateExpression,
            results => Result<bool>.Success(results.Any()),
            results => Result<bool>.Success(results.Any(x => x.Result.Value))
        );

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context);

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(AnyExpression),
            "Returns an indicator whether any item from the (enumerable) context value conform to the predicate, optionally using a predicate",
            "True when any item in the enumerable conform to the predicate, otherwise false",
            "This will be returned in case no error occurs",
            "Context is not of type enumerable, Predicate did not return a boolean value",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            false
        );
}

