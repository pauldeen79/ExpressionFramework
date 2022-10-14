namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITakeExpression : IExpression
{
    [Required]
    IExpression CountExpression { get; }
}
