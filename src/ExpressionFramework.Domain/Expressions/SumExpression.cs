namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Computes the sum from an enumerable context value using an optional selection expression")]
[ContextType(typeof(IEnumerable))]
[ContextDescription("The enumerable value to summarize")]
[ContextRequired(true)]
[ParameterDescription(nameof(SelectorExpression), "Optional expression to select value from each item")]
[ParameterRequired(nameof(SelectorExpression), false)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Sum (could be decimal, double, float, long or int)", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context cannot be empty, Context must be of type IEnumerable, Could only compute sum of numeric values")]
public partial record SumExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is IEnumerable e
            ? EnumerableExpression.GetTypedResultFromEnumerable(e, e => e
                .Select(x => SelectorExpression == null
                    ? Result<object?>.Success(x)
                    : SelectorExpression.Evaluate(x)))
                .Transform(result => result.IsSuccessful()
                    ? Sum(result.Value!)
                    : Result<object?>.FromExistingResult(result))
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

        if (!e.OfType<object>().All(x => x is decimal)
            && !e.OfType<object>().All(x => x is double)
            && !e.OfType<object>().All(x => x is float)
            && !e.OfType<object>().All(x => x is long)
            && !e.OfType<object>().All(x => x is int || x is short || x is byte))
        {
            yield return new ValidationResult("Could only compute sum of numeric values");
        }
    }

    private static Result<object?> Sum(IEnumerable<object?> value)
    {
        if (value.All(x => x is decimal))
        {
            return Result<object?>.Success(value.Cast<decimal>().Sum());
        }

        if (value.All(x => x is double))
        {
            return Result<object?>.Success(value.Cast<double>().Sum());
        }

        if (value.All(x => x is float))
        {
            return Result<object?>.Success(value.Cast<float>().Sum());
        }

        if (value.All(x => x is long))
        {
            return Result<object?>.Success(value.Cast<long>().Sum());
        }

        if (value.All(x => x is int || x is short || x is byte))
        {
            return Result<object?>.Success(value.Cast<int>().Sum());
        }

        return Result<object?>.Invalid("Could only compute sum of numeric values");
    }
}
