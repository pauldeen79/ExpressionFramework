namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the length of the (string) context")]
[UsesContext(true)]
[ContextDescription("String to get the length from")]
[ContextRequired(true)]
[ContextType(typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(int), "The length of the (string) context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string")]
public partial record StringLengthExpression : ITypedExpression<int>
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? Result<object?>.Success(s.Length)
            : Result<object?>.Invalid("Context must be of type string");

    public Result<int> EvaluateTyped(object? context)
        => context is string s
            ? Result<int>.Success(s.Length)
            : Result<int>.Invalid("Context must be of type string");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => StringExpression.ValidateContext(context);
}

