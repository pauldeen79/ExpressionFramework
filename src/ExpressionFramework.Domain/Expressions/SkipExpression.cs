namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Skips a number of items on an enumerable context value")]
[ContextType(typeof(IEnumerable))]
[ContextDescription("The enumerable value to skip elements from")]
[ContextRequired(true)]
[ParameterDescription(nameof(CountExpression), "Number of items to skip")]
[ParameterRequired(nameof(CountExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with skipped items", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, CountExpression did not return an integer, Context must be of type IEnumerable")]
public partial record SkipExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var countResult = CountExpression.Evaluate(context);
        if (!countResult.IsSuccessful())
        {
            return countResult;
        }

        if (countResult.Value is not int count)
        {
            return Result<object?>.Invalid("CountExpression did not return an integer");
        }

        return context is IEnumerable e
            ? EnumerableExpression.GetResultFromEnumerable(e, e => e
                .Skip(count)
                .Select(x => Result<object?>.Success(x)))
            : context.Transform(x => Result<object?>.Invalid(x == null
                ? "Context cannot be empty"
                : "Context is not of type enumerable"));
    }

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => EnumerableExpression.ValidateContext(context, () => IntExpression.ValidateParameter(context, CountExpression, nameof(CountExpression)));

    public SkipExpression(int count) : this(new ConstantExpression(count)) { }
}
