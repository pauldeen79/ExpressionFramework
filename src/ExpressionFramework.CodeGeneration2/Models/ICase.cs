namespace ExpressionFramework.CodeGeneration.Models;

public interface ICase
{
    [Required][ValidateObject] IEvaluatable Condition { get; }
    [Required][ValidateObject] IExpression Expression { get; }
}
