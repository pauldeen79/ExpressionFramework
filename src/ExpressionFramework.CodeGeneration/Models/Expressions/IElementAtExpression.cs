namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IElementAtExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression IndexExpression { get; }
}
