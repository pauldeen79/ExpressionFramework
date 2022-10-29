namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITakeExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression CountExpression { get; }
}
