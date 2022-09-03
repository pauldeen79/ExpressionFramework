namespace CodeGenerationNext.Models;

public interface IAggregateExpression : IExpression
{
    IReadOnlyCollection<IExpression> Expressions { get; }
    IReadOnlyCollection<ICondition> ExpressionConditions { get; }
    IAccumulatorExpression Accumulator { get; }
}
