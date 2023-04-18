namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISequenceExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required]
    IReadOnlyCollection<IExpression> Expressions { get; }
}
