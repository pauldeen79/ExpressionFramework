namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAggregateExpression : IExpression
{
    [Required]
    IReadOnlyCollection<IExpression> SubsequentExpressions { get; }
    [Required]
    IAggregator Aggregator { get; }
}
