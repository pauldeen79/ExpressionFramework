namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOrderByExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject] IReadOnlyCollection<ITypedExpression<ISortOrder>> SortOrderExpressions { get; }
}
