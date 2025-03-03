namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("This expression returns the value of the source expression cast to the specified type")]
public interface ICastExpression<T> : IExpression, ITypedExpression<T>
{
    [Required][ValidateObject][Description("Expression to cast")] IExpression SourceExpression { get; }
}
