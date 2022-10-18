namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISingleExpression : IExpression
{
    IExpression? PredicateExpression { get; }
}
