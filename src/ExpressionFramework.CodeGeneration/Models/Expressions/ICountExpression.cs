namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ICountExpression : IExpression
{
    IExpression? PredicateExpression { get; }
}
