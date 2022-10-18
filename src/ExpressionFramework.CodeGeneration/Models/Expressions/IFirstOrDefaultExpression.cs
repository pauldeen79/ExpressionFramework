namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFirstOrDefaultExpression : IExpression
{
    IExpression? Predicate { get; }
    IExpression? DefaultExpression { get; }
}
