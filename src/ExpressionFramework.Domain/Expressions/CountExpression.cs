namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(CountExpression))]
public partial record CountExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetScalarValueWithDefault
        (
            context,
            PredicateExpression,
            results => Result<object?>.Success(results.Count()),
            results => Result<object?>.Success(results.Count(x => x.Result.Value))
        );

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context);

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(CountExpression),
            "Gets the number of items from the (enumerable) context value, optionally using a predicate",
            "Number of items in the enumerable that conforms to the predicate",
            "This will be returned in case no error occurs",
            "Context is not of type enumerable, Predicate did not return a boolean value",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            false
        );
}

