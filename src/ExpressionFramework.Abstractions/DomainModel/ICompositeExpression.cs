namespace ExpressionFramework.Abstractions.DomainModel;

public interface ICompositeExpression : IExpression
{
    IReadOnlyCollection<IExpression> Expressions { get; }
    ICompositeFunction CompositeFunction { get; }
}
