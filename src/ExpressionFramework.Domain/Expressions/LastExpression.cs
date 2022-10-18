namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets the last value from the (enumerable) context value, optionally using a predicate to select an item")]
[UsesContext(true)]
[ContextDescription("Enumerable value to use")]
[ContextType(typeof(IEnumerable))]
[ContextRequired(true)]
[ParameterDescription(nameof(Predicate), "Optional predicate to use")]
[ParameterRequired(nameof(Predicate), false)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Value of the last item of the enumerable that conforms to the predicate", "This will be returned in case the enumerable is not empty, and no error occurs")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context is not of type enumerable, Enumerable is empty, Predicate did not return a boolean value, None of the items conform to the supplied predicate")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok")]
public partial record LastExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetScalarValueWithoutDefault(context, Predicate, results => results.Last(), results => results.Last(x => x.Result.Value).Item);

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context, () => EnumerableExpression.ValidateEmptyEnumerable(context));
}
