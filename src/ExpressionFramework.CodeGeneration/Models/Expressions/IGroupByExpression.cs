namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IGroupByExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression KeySelectorExpression { get; }
}
