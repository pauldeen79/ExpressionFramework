namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOperatorExpression : IExpression
{
    IExpression LeftExpression { get; }
    IExpression RightExpression { get; }
    IOperator Operator { get; }
}
