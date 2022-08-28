namespace ExpressionFramework.Abstractions.DomainModel;

public interface ISwitchExpression : IExpression
{
    IReadOnlyCollection<ICase> Cases { get; }
    IExpression DefaultExpression { get; }
}
