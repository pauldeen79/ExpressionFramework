namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(FirstOrDefaultExpression))]
public partial record FirstOrDefaultExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetScalarValueWithDefault
        (
            context,
            PredicateExpression,
            results => Result<object?>.Success(results.First()),
            results => Result<object?>.Success(results.First(x => x.Result.Value).Item),
            context => EnumerableExpression.GetDefaultValue(DefaultExpression, context)
        );

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context);

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(FirstOrDefaultExpression),
            "Gets the first value from the (enumerable) context value, optionally using a predicate to select an item",
            "Value of the first item of the enumerable that conforms to the predicate, or the default value",
            "This will be returned in case the enumerable is not empty, and no error occurs",
            "Context is not of type enumerable, Predicate did not return a boolean value",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            true
        );
}

