namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISelectExpression : IExpression
{
    IExpression Selector { get; }
}
