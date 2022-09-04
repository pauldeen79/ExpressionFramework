namespace CodeGenerationNext.Models.Expressions;

public interface IChainedExpression : IExpression
{
    [Required]
    IReadOnlyCollection<IExpression> Expressions { get; }
}
