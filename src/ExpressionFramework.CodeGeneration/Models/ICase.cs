namespace ExpressionFramework.CodeGeneration.Models;

public interface ICase
{
    [Required]
    IEvaluatable Condition { get; }
    [Required]
    IExpression Expression { get; }
}
