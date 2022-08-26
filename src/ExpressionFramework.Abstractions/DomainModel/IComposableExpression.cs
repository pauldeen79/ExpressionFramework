namespace ExpressionFramework.Abstractions.DomainModel;

public interface IComposableExpression : IExpression
{
    IReadOnlyCollection<IExpression> Expressions { get; }
}
