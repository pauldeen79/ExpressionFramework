namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOrderByExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    [Required]
    IReadOnlyCollection<IExpression> SortOrderExpressions { get; }
}
