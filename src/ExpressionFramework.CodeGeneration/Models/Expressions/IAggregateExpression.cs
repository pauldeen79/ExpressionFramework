namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAggregateExpression : IExpression
{
    [Required]
    IExpression FirstExpression { get; }
    [Required]
    IReadOnlyCollection<IExpression> SubsequentExpressions { get; }
    [Required]
    IAggregator Aggregator { get; }
}
