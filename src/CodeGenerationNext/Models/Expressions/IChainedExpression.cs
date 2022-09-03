namespace CodeGenerationNext.Models.Expressions;

public interface IChainedExpression : IExpression
{
    IReadOnlyCollection<IExpression> Expressions { get; }
}
