namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Sorts items from an enumerable expression using sort expressions")]
public interface IOrderByExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject] IReadOnlyCollection<ITypedExpression<ISortOrder>> SortOrderExpressions { get; }
}
