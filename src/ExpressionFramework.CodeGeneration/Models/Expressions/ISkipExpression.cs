namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISkipExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression CountExpression { get; }
}
