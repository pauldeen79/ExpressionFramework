namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IGroupByExpression : IExpression
{
    [Required]
    IExpression KeySelectorExpression { get; }
}
