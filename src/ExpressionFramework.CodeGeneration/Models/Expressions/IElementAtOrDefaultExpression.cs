namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IElementAtOrDefaultExpression : IExpression
{
    [Required]
    IExpression IndexExpression { get; }
    IExpression? DefaultExpression { get; }
}
