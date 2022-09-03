namespace CodeGenerationNext.Models;

public interface IChainedExpression : IExpression
{
    IReadOnlyCollection<IExpression> Expressions { get; }
}
