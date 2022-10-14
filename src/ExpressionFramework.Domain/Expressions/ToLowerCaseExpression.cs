namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Converts the context to lower case")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("String to convert to lower case")]
[ExpressionContextRequired(true)]
[ExpressionContextType(typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The value of the context converted to lower case", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string")]
public partial record ToLowerCaseExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? Result<object?>.Success(s.ToLower())
            : Result<object?>.Invalid("Context must be of type string");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpression.ValidateContext(context);
}
