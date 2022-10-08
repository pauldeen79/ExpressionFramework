namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOrderByExpression : IExpression
{
    [Required]
    IReadOnlyCollection<ISortOrder> SortOrders { get; }
}
