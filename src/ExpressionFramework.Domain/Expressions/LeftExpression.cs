namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets a number of characters of the start of a string value of the context")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("String to get the first characters for")]
[ExpressionContextRequired(true)]
[ExpressionContextType(typeof(string))]
[ParameterDescription(nameof(Length), "Number of characters to use")]
[ParameterRequired(nameof(Length), true)]
[ReturnValue(ResultStatus.Ok, "The first characters of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string")]
public partial record LeftExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? GetLeftValueFromString(s)
            : Result<object?>.Invalid("Context must be of type string");

    private Result<object?> GetLeftValueFromString(string s)
        => s.Length >= Length
            ? Result<object?>.Success(s.Substring(0, Length))
            : Result<object?>.Invalid("Length must refer to a location within the string");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpression.ValidateContext(context);
}

