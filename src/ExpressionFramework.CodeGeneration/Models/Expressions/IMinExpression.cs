namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IMinExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    IExpression? SelectorExpression { get; }
}
