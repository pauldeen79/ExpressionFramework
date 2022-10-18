namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFirstExpression : IExpression
{
    IExpression? Predicate { get; }
}
