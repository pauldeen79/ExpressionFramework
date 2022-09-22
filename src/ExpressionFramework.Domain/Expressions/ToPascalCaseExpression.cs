namespace ExpressionFramework.Domain.Expressions;

public partial record ToPascalCaseExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? Result<object?>.Success(ToPascalCase(s))
            : Result<object?>.Invalid("Context must be of type string");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
    {
        if (context is not string)
        {
            yield return new ValidationResult("Context must be of type string");
        }
    }

    private string? ToPascalCase(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return value.Substring(0, 1).ToLowerInvariant() + value.Substring(1);
        }

        return value;
    }
}
