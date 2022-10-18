namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(SingleExpression))]
public partial record SingleExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetScalarValueWithoutDefault
        (
            context,
            PredicateExpression,
            results => Result<object?>.Success(results.Single()),
            results => Result<object?>.Success(results.Single(x => x.Result.Value).Item),
            items => items.Count(x => PredicateExpression == null || PredicateExpression.Evaluate(x).TryCast<bool>("Predicate did not return a boolean value").Value) > 1
                ? Result<IEnumerable<object?>>.Invalid("Sequence contains more than one element")
                : Result<IEnumerable<object?>>.Success(items)
        );

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context, () => EnumerableExpression.ValidateEmptyEnumerable(context));

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(SingleExpression),
            "Gets a single value from the (enumerable) context value, optionally using a predicate to select an item",
            "Value of the single item of the enumerable that conforms to the predicate",
            "This will be returned in case the enumerable contains a single element, and no error occurs",
            "Context is not of type enumerable, Enumerable is empty, Predicate did not return a boolean value, None of the items conform to the supplied predicate, Sequence contains one than one element",
            "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok",
            false
        );
}

