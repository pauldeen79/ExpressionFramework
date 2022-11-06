namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISumExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    IExpression? SelectorExpression { get; }
}
