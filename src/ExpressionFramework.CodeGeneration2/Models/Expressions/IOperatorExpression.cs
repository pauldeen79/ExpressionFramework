namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOperatorExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject] IExpression LeftExpression { get; }
    [Required][ValidateObject] IExpression RightExpression { get; }
    [Required][ValidateObject] IOperator Operator { get; }
}
