namespace ExpressionFramework.Abstractions.DomainModel;

public interface IChainedExpression : IExpression
{
    IReadOnlyCollection<IExpression> Expressions { get; }
}
