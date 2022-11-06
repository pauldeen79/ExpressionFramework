namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITrimExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    IExpression? TrimCharsExpression { get; }
}
