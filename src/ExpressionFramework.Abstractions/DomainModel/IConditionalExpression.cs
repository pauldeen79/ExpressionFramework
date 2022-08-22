namespace ExpressionFramework.Abstractions.DomainModel;

public interface IConditionalExpression : IExpression
{
    IReadOnlyCollection<ICondition> Conditions { get; }
    IExpression ResultExpression { get; }
    IExpression DefaultExpression { get; }
}
