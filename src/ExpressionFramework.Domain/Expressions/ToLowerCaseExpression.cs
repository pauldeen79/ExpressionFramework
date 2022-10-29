namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Converts the context to lower case")]
[UsesContext(true)]
[ContextDescription("String to convert to lower case")]
[ContextRequired(true)]
[ContextType(typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The value of the context converted to lower case", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string")]
public partial record ToLowerCaseExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? Result<object?>.Success(s.ToLower())
            : Result<object?>.Invalid("Context must be of type string");

    public Result<string> EvaluateTyped(object? context)
        => context is string s
            ? Result<string>.Success(s.ToLower())
            : Result<string>.Invalid("Context must be of type string");
}
