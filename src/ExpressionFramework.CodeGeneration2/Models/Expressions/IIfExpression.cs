namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IIfExpression : IExpression
{
    [Required][ValidateObject] IEvaluatable Condition { get; }
    [Required][ValidateObject] IExpression ResultExpression { get; }
    [ValidateObject] IExpression? DefaultExpression { get; }
}
