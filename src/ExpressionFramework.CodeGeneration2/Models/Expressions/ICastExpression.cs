namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ICastExpression<T> : IExpression, ITypedExpression<T>
{
    [Required][ValidateObject] IExpression SourceExpression { get; }
}
