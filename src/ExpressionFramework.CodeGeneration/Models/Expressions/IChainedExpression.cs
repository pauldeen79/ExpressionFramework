namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IChainedExpression : IExpression
{
    [Required]
    IReadOnlyCollection<IExpression> Expressions { get; }
}
