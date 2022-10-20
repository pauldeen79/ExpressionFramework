namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISumExpression : IExpression
{
    IExpression? SelectorExpression { get; }
}
