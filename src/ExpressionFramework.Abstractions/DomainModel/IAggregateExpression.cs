namespace ExpressionFramework.Abstractions.DomainModel;

public interface IAggregateExpression : IExpression
{
    IReadOnlyCollection<IExpression> Expressions { get; }
    IReadOnlyCollection<ICondition> ExpressionConditions { get; }
    IAggregateFunction AggregateFunction { get; }
}
