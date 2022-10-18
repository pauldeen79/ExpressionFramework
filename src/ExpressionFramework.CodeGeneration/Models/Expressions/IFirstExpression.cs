namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFirstExpression : IExpression
{
    IExpression? PredicateExpression { get; }
}
