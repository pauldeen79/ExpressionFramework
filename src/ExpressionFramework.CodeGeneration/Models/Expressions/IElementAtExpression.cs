namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IElementAtExpression : IExpression
{
    [Required]
    IExpression IndexExpression { get; }
}
