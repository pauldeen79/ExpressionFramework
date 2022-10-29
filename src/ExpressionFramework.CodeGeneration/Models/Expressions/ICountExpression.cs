namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ICountExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    IExpression? PredicateExpression { get; }
}
