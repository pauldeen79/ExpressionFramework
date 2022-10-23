namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IIfExpression : IExpression
{
    [Required]
    IEvaluatable Condition { get; }
    [Required]
    IExpression ResultExpression { get; }
    IExpression? DefaultExpression { get; }
}
