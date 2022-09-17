namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IEvaluatableExpression : IExpression
{
    [Required]
    IEvaluatable Condition { get; }
}
