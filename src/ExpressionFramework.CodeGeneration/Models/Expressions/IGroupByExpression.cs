namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IGroupByExpression : IExpression
{
    IExpression KeySelectorExpression { get; }
}
