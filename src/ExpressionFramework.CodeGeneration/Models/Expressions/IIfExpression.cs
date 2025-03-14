namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Evaluates a condition")]
public interface IIfExpression : IExpression
{
    [Required][ValidateObject][Description("Condition to evaluate")] IEvaluatable Condition { get; }
    [Required][ValidateObject][Description("Expression to use when the condition evaluates to true")] IExpression ResultExpression { get; }
    [ValidateObject][Description("Optional expression to use when the condition evaluates to false. When left empty, an empty expression will be used.")] IExpression ? DefaultExpression { get; }
}
