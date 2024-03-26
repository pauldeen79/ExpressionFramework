namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IGroupByExpression : IExpression
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject] IExpression KeySelectorExpression { get; }
}
