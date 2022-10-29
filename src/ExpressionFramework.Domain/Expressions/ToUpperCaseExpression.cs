namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Converts the context to upper case")]
[UsesContext(true)]
[ContextDescription("String to convert to upper case")]
[ContextRequired(true)]
[ContextType(typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The value of the context converted to upper case", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string")]
public partial record ToUpperCaseExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? Result<object?>.Success(s.ToUpper())
            : Result<object?>.Invalid("Context must be of type string");

    public Result<string> EvaluateTyped(object? context)
        => context is string s
            ? Result<string>.Success(s.ToUpper())
            : Result<string>.Invalid("Context must be of type string");
}
