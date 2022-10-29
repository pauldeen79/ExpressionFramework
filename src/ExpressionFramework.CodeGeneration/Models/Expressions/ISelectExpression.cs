namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISelectExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression SelectorExpression { get; }
}
