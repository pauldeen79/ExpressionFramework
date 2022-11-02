namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITrimEndExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    IExpression? TrimCharsExpression { get; }
}
