namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAnyExpression : IExpression
{
    IExpression? PredicateExpression { get; }
}
