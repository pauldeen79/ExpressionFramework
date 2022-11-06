namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFirstOrDefaultExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    IExpression? PredicateExpression { get; }
    IExpression? DefaultExpression { get; }
}
