namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Trims the start and end characters of the context")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("String to trim the start and end characters of")]
[ExpressionContextRequired(true)]
[ExpressionContextType(typeof(string))]
[ParameterDescription(nameof(TrimChars), "Optional trim characters to use. When empty, space will be used")]
[ParameterRequired(nameof(TrimChars), false)]
[ReturnValue(ResultStatus.Ok, "The trim start and end value of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string")]
public partial record TrimExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? Result<object?>.Success(Trim(s))
            : Result<object?>.Invalid("Context must be of type string");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpressionBase.ValidateContext(context);

    public TrimExpression() : this(default(IEnumerable<char>)) { }

    private string Trim(string s) => TrimChars == null ? s.Trim() : s.Trim(TrimChars.ToArray());
}

