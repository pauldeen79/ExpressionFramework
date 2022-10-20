namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Transforms items from an enumerable context value using an expression")]
[ContextType(typeof(IEnumerable))]
[ContextDescription("The enumerable value to transform elements for")]
[ContextRequired(true)]
[ParameterDescription(nameof(SelectorExpression), "Expression to use on each item")]
[ParameterRequired(nameof(SelectorExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with transformed items", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Context is not of type enumerable")]
public partial record SelectExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is IEnumerable e
            ? EnumerableExpression.GetResultFromEnumerable(e, e => e
                .Select(x => SelectorExpression.Evaluate(x)))
            : context.Transform(x => Result<object?>.Invalid(x == null
                ? "Context cannot be empty"
                : "Context is not of type enumerable"));

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
    {
        if (context == null)
        {
            yield return new ValidationResult("Context cannot be empty");
            yield break;
        }

        if (context is not IEnumerable e)
        {
            yield return new ValidationResult("Context is not of type enumerable");
            yield break;
        }

        var index = 0;
        foreach (var itemResult in e.OfType<object>().Select(x => SelectorExpression.Evaluate(x)))
        {
            if (itemResult.Status == ResultStatus.Invalid)
            {
                yield return new ValidationResult($"SelectorExpression returned an invalid result on item {index}. Error message: {itemResult.ErrorMessage}");
            }

            index++;
        }
    }
}

