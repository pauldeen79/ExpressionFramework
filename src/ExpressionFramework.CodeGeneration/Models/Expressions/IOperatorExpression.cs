namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOperatorExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    IExpression LeftExpression { get; }
    [Required]
    IExpression RightExpression { get; }
    IOperator Operator { get; }
}
