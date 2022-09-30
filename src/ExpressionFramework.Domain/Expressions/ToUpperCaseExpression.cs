namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Converts the context to upper case")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("String to convert to upper case")]
[ExpressionContextRequired(true)]
[ExpressionContextType(typeof(string))]
[ReturnValue(ResultStatus.Ok, "The value of the context converted to upper case", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "This result will be returned when the context is not of type string")]
public partial record ToUpperCaseExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? Result<object?>.Success(s.ToUpper())
            : Result<object?>.Invalid("Context must be of type string");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
    {
        if (context is not string)
        {
            yield return new ValidationResult("Context must be of type string");
        }
    }
}
