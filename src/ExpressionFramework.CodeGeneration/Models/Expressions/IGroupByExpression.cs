namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IGroupByExpression : IExpression
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    [Required]
    IExpression KeySelectorExpression { get; }
}
