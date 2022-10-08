namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets a number of characters from the specified position of a string value of the context")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("String to get a number of characters for")]
[ExpressionContextRequired(true)]
[ExpressionContextType(typeof(string))]
[ParameterDescription(nameof(Index), "Zero-based start position of the characters to return")]
[ParameterRequired(nameof(Index), true)]
[ParameterDescription(nameof(Length), "Number of characters to use")]
[ParameterRequired(nameof(Length), true)]
[ReturnValue(ResultStatus.Ok, "A set of characters of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string")]
public partial record SubstringExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? GetLeftValueFromString(s)
            : Result<object?>.Invalid("Context must be of type string");

    private Result<object?> GetLeftValueFromString(string s)
        => s.Length >= Index + Length
            ? Result<object?>.Success(s.Substring(Index, Length))
            : Result<object?>.Invalid("Index and length must refer to a location within the string");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpression.ValidateContext(context);
}

