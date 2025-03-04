namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Evaluates the specified condition, and returns the result")]
public interface IEvaluatableExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject][Description("Condition to evaluate on the expression")] IEvaluatable Condition { get; }
    [Required][ValidateObject][Description("Expression to use")] IExpression Expression { get; }
}
