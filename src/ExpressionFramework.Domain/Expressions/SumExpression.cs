namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(SumExpression))]
public partial record SumExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is IEnumerable e
            ? EnumerableExpression.GetTypedResultFromEnumerable(e, x => x
                .Select(y => SelectorExpression == null
                    ? Result<object?>.Success(y)
                    : SelectorExpression.Evaluate(y)))
                .Transform(result => result.IsSuccessful()
                    ? Sum(result.Value!)
                    : Result<object?>.FromExistingResult(result))
            : context.Transform(x =>  Result<object?>.Invalid(x == null
                ? "Context cannot be empty"
                : "Context must be of type IEnumerable"));

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

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(SumExpression),
            "Gets the sum from the (enumerable) context value, optionally using a selector expression",
            "Sum (could be decimal, double, float, long or int)",
            "This will be returned in case no error occurs",
            "Context cannot be empty, Context must be of type IEnumerable, Could only compute sum of numeric values",
            "This status (or any other status not equal to Ok) will be returned in case the selector evaluation returns something else than Ok",
            false
        );

    private static Result<object?> Sum(IEnumerable<object?> value)
    {
        if (value.All(x => x is decimal))
        {
            return Result<object?>.Success(value.Select(x => Convert.ToDecimal(x)).Sum());
        }

        if (value.All(x => x is double))
        {
            return Result<object?>.Success(value.Select(x => Convert.ToDouble(x)).Sum());
        }

        if (value.All(x => x is float))
        {
            return Result<object?>.Success(value.Select(x => Convert.ToSingle(x)).Sum());
        }

        if (value.All(x => x is long))
        {
            return Result<object?>.Success(value.Select(x => Convert.ToInt64(x)).Sum());
        }

        if (value.All(x => x is int || x is short || x is byte))
        {
            return Result<object?>.Success(value.Select(x => Convert.ToInt32(x)).Sum());
        }

        return Result<object?>.Invalid("Could only compute sum of numeric values");
    }
}
