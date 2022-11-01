namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOrderByExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IReadOnlyCollection<IExpression> SortOrderExpressions { get; }
}
