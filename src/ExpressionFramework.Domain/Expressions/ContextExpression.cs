namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the value of the context")]
[UsesContext(true)]
[ContextDescription("Context value to use")]
[ContextType(typeof(object))]
[ContextRequired(true)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Value of the context", "This result will always be returned")]
public partial record ContextExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(context);
}
