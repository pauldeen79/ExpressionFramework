namespace ExpressionFramework.Abstractions.DomainModel;

public interface ICase
{
    IReadOnlyCollection<ICondition> Conditions { get; }
    IExpression Expression { get; }
}
