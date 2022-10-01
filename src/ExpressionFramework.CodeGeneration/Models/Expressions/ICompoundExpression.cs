namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ICompoundExpression : IExpression
{
    IExpression SecondExpression { get; }
    IAggregator Aggregator { get; }
}
