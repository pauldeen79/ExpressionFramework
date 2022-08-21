namespace ExpressionFramework.Abstractions.DomainModel;

public interface ICompositeExpression : IExpression
{
    IReadOnlyCollection<IExpression> Expressions { get; }
    IReadOnlyCollection<ICondition> ExpressionConditions { get; }
    ICompositeFunction CompositeFunction { get; }
}
