namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITypedChainedExpression<T> : IExpression, ITypedExpression<T>
{
    [Required][ValidateObject] IReadOnlyCollection<IExpression> Expressions { get; }
}
