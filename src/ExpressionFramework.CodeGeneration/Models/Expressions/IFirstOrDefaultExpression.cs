namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFirstOrDefaultExpression : IExpression
{
    IExpression? PredicateExpression { get; }
    IExpression? DefaultExpression { get; }
}
