namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IRightExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression LengthExpression { get; }
}
