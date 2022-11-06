namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITrimStartExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    IExpression? TrimCharsExpression { get; }
}
