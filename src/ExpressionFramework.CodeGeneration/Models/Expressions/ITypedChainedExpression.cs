namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITypedChainedExpression<T> : IExpression, ITypedExpression<T>
{
    [Required]
    IReadOnlyCollection<IExpression> Expressions { get; }
}
