namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAggregateExpression : IExpression
{
    [Required]
    IReadOnlyCollection<IExpression> Expressions { get; }
    [Required]
    IAggregator Aggregator { get; }
}
