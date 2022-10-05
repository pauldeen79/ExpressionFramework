namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IWhereExpression : IExpression
{
    IExpression PredicateExpression { get; }
}
