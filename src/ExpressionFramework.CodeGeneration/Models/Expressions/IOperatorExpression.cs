namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOperatorExpression : IExpression
{
    [Required]
    IExpression LeftExpression { get; }
    [Required]
    IOperator Operator { get; }
    [Required]
    IExpression RightExpression { get; }
}
