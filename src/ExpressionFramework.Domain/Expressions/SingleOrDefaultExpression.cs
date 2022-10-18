namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets a single value from the (enumerable) context value or a default value, optionally using a predicate to select an item")]
[UsesContext(true)]
[ContextDescription("Enumerable value to use")]
[ContextType(typeof(IEnumerable))]
[ContextRequired(true)]
[ParameterDescription(nameof(PredicateExpression), "Optional predicate to use")]
[ParameterRequired(nameof(PredicateExpression), false)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Value of the single item of the enumerable that conforms to the predicate, or the default value", "This will be returned in case the enumerable contains a single element, and no error occurs")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context is not of type enumerable, Predicate did not return a boolean value, Sequence contains one than one element")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok")]
public partial record SingleOrDefaultExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetScalarValueWithDefault
        (
            context,
            PredicateExpression,
            results => results.Single(),
            results => results.Single(x => x.Result.Value).Item,
            context => EnumerableExpression.GetDefaultValue(DefaultExpression, context),
            items => items.Count(x => PredicateExpression == null || PredicateExpression.Evaluate(x).TryCast<bool>("Predicate did not return a boolean value").Value) > 1
                ? Result<IEnumerable<object?>>.Invalid("Sequence contains more than one element")
                : Result<IEnumerable<object?>>.Success(items)
        );

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context);
}

