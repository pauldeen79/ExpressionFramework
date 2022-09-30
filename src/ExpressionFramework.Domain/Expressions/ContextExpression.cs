namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the value of the context")]
[ExpressionUsesContext(true)]
[ExpressionContextDescription("Context value to use")]
[ExpressionContextType(typeof(object))]
[ExpressionContextRequired(true)]
[ReturnValue(ResultStatus.Ok, "Value of the context", "This result will always be returned")]
public partial record ContextExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(context);
}
