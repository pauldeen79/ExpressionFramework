namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Transforms items from an enumerable context value using an expression")]
[ExpressionContextType(typeof(IEnumerable))]
[ExpressionContextDescription("The enumerable value to transform elements for")]
[ExpressionContextRequired(true)]
[ParameterDescription(nameof(Selector), "Expression to use on each item")]
[ParameterRequired(nameof(Selector), true)]
[ReturnValue(ResultStatus.Ok, "Enumerable with transformed items", "This result will be returned when the context is enumerble")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Context must be of type IEnumerable")]
public partial record SelectExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is IEnumerable e
            ? EnumerableExpression.GetResultFromEnumerable(e, e => e
                .Select(x => Selector.Evaluate(x)))
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

        var index = 0;
        foreach (var itemResult in e.OfType<object>().Select(x => Selector.Evaluate(x)))
        {
            if (itemResult.Status == ResultStatus.Invalid)
            {
                yield return new ValidationResult($"SelectExpression returned an invalid result on item {index}. Error message: {itemResult.ErrorMessage}");
            }

            index++;
        }
    }
}

