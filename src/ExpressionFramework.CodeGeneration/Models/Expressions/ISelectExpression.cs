namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISelectExpression : IExpression
{
    IExpression SelectorExpression { get; }
}
