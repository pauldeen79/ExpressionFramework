namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOrderByExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    [Required]
    IMultipleTypedExpressions<ISortOrder> SortOrderExpressions { get; }
}
