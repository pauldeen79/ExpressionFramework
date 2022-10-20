namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IMaxExpression : IExpression
{
    IExpression? SelectorExpression { get; }
}
