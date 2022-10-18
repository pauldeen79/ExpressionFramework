namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ILastOrDefaultExpression : IExpression
{
    IExpression? Predicate { get; }
    IExpression? DefaultExpression { get; }
}
