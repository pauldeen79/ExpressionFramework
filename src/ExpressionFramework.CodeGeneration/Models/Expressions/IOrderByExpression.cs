namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOrderByExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IReadOnlyCollection<IExpression> SortOrderExpressions { get; }
}
