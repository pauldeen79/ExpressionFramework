namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISingleOrDefaultExpression : IExpression
{
    IExpression? PredicateExpression { get; }
    IExpression? DefaultExpression { get; }
}
