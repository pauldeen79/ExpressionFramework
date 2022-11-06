namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISingleOrDefaultExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    IExpression? PredicateExpression { get; }
    IExpression? DefaultExpression { get; }
}
