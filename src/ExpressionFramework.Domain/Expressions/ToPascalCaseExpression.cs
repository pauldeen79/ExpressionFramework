namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Converts the context to pascal case")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("String to convert to pascal case")]
[ExpressionContextRequired(true)]
[ExpressionContextType(typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The value of the context converted to pascal case", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string")]
public partial record ToPascalCaseExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? Result<object?>.Success(ToPascalCase(s))
            : Result<object?>.Invalid("Context must be of type string");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpression.ValidateContext(context);

    private string? ToPascalCase(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return value.Substring(0, 1).ToLowerInvariant() + value.Substring(1);
        }

        return value;
    }
}
