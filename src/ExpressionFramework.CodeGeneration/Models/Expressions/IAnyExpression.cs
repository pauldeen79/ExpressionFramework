namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAnyExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    IExpression? PredicateExpression { get; }
}
