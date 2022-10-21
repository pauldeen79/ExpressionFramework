namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISequenceExpression : IExpression
{
    [Required]
    IReadOnlyCollection<IExpression> Expressions { get; }
}
