namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Filters an enumerable context value on type")]
[ExpressionContextType(typeof(IEnumerable))]
[ExpressionContextDescription("The enumerable value to filter")]
[ExpressionContextRequired(true)]
[ParameterDescription(nameof(Type), "Type to filter on")]
[ParameterRequired(nameof(Type), true)]
[ReturnValue(ResultStatus.Ok, "Enumerable with items that are of the specified type", "This result will be returned when the context is enumerble")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Context must be of type IEnumerable")]
public partial record OfTypeExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is IEnumerable e
            ? EnumerableExpression.GetResultFromEnumerable(e, e => e
                .Select(x => new { Item = x, Result = Result<object?>.Success(x != null && Type.IsInstanceOfType(x)) })
                .Where(x => !x.Result.IsSuccessful() || x.Result.Value.IsTrue())
                .Select(x => x.Result.IsSuccessful() ? Result<object?>.Success(x.Item) : x.Result))
            : Result<object?>.Invalid("Context must be of type IEnumerable");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
    {
        if (context == null)
        {
            yield return new ValidationResult("Context cannot be empty");
            yield break;
        }

        if (context is not IEnumerable e)
        {
            yield return new ValidationResult("Context must be of type IEnumerable");
            yield break;
        }
    }
}

