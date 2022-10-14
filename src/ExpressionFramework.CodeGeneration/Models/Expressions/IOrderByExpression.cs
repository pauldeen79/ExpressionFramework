namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOrderByExpression : IExpression
{
    [Required]
    IReadOnlyCollection<IExpression> SortOrderExpressions { get; }
}
