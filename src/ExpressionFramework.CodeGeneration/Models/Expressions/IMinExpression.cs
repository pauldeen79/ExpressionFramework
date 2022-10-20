namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IMinExpression : IExpression
{
    IExpression? SelectorExpression { get; }
}
