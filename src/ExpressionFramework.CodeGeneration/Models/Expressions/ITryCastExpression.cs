namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITryCastExpression<T> : IExpression, ITypedExpression<T>
{
    [Required][ValidateObject] IExpression SourceExpression { get; }
    [ValidateObject] ITypedExpression<T>? DefaultExpression { get; }
}
