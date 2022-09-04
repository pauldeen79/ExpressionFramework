namespace CodeGenerationNext.Models.Expressions;

public interface IAggregateExpression : IExpression
{
    [Required]
    IReadOnlyCollection<IExpression> Expressions { get; }
    [Required]
    IReadOnlyCollection<ICondition> ExpressionConditions { get; }
    [Required]
    IAccumulatorExpression Accumulator { get; }
}
