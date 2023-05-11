namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ICastExpression<T> : IExpression, ITypedExpression<T>
{
    [Required]
    IExpression SourceExpression { get; }
}
