namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITryCastExpression<T> : IExpression, ITypedExpression<T>
{
    [Required]
    IExpression SourceExpression { get; }
    ITypedExpression<T>? DefaultExpression { get; }
}
