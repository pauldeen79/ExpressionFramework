namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Skips a number of items on an enumerable context value")]
[ExpressionContextType(typeof(IEnumerable))]
[ExpressionContextDescription("The enumerable value to skip elements from")]
[ExpressionContextRequired(true)]
[ParameterDescription(nameof(Count), "Number of items to skip")]
[ParameterRequired(nameof(Count), true)]
[ReturnValue(ResultStatus.Ok, "Enumerable with skipped items", "This result will be returned when the context is enumerble")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Context must be of type IEnumerable")]
public partial record SkipExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is IEnumerable e
            ? EnumerableExpression.GetResultFromEnumerable(e, e => e
                .Skip(Count)
                .Select(x => Result<object?>.Success(x)))
            : Result<object?>.Invalid("Context must be of type IEnumerable");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context);
}
