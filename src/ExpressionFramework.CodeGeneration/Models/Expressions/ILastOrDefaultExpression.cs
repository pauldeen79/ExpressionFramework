namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ILastOrDefaultExpression : IExpression
{
    IExpression? PredicateExpression { get; }
    IExpression? DefaultExpression { get; }
}
