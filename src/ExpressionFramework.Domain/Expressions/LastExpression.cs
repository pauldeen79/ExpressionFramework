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
    {
        if (context is not IEnumerable e)
        {
            return Result<object?>.Invalid("Context is not of type enumerable");
        }

        var items = e.OfType<object?>();

        if (Predicate == null)
        {
            if (!items.Any())
            {
                return Result<object?>.Invalid("Enumerable is empty");
            }

            return Result<object?>.Success(items.Last());
        }

        var results = items.Select(x => new
        {
            Item = x,
            Result = Predicate.Evaluate(x).TryCast<bool>("Predicate did not return a boolean value")
        }).TakeWhileWithFirstNonMatching(x => x.Result.IsSuccessful());

        if (results.Any(x => !x.Result.IsSuccessful()))
        {
            // Error in predicate evaluation
            return Result<object?>.FromExistingResult(results.First(x => !x.Result.IsSuccessful()).Result);
        }

        if (!results.Any(x => x.Result.Value))
        {
            return Result<object?>.Invalid("None of the items conform to the supplied predicate");
        }

        return Result<object?>.Success(results.Last(x => x.Result.Value).Item);
    }

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context, () => EnumerableExpression.ValidateEmptyEnumerable(context));
}
