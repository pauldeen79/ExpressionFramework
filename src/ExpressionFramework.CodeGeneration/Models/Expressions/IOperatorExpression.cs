namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Evaluates an operator")]
public interface IOperatorExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject][Description("Left expression to use on operator")] IExpression LeftExpression { get; }
    [Required][ValidateObject][Description("Right expression to use on operator")] IExpression RightExpression { get; }
    [Required][ValidateObject][Description("Operator to evaluate")] IOperator Operator { get; }
}
