namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets the first value from the (enumerable) context value or a default value, optionally using a predicate to select an item")]
[UsesContext(true)]
[ContextDescription("Enumerable value to use")]
[ContextType(typeof(IEnumerable))]
[ContextRequired(true)]
[ParameterDescription(nameof(Predicate), "Optional predicate to use")]
[ParameterRequired(nameof(Predicate), false)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Value of the first item of the enumerable that conforms to the predicate, or the default value", "This will be returned in case the enumerable is not empty, and no error occurs")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context is not of type enumerable, Predicate did not return a boolean value")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case the predicate evaluation returns something else than Ok")]
public partial record FirstOrDefaultExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetScalarValueWithDefault
        (
            context,
            Predicate,
            results => results.First(),
            results => results.First(x => x.Result.Value).Item,
            GetDefaultValue
        );

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context);

    private Result<object?> GetDefaultValue(object? context)
        => DefaultExpression == null
            ? new EmptyExpression().Evaluate(context)
            : DefaultExpression.Evaluate(context);
}

