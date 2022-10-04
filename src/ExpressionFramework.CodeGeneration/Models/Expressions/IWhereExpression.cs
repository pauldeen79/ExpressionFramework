namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IWhereExpression : IExpression
{
    IExpression Predicate { get; }
}
