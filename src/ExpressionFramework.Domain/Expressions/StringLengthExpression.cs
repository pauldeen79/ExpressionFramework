namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the length of the (string) context")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("String to get the length from")]
[ExpressionContextRequired(true)]
[ExpressionContextType(typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(int), "The length of the (string) context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string")]
public partial record StringLengthExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? Result<object?>.Success(s.Length)
            : Result<object?>.Invalid("Context must be of type string");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpression.ValidateContext(context);
}

