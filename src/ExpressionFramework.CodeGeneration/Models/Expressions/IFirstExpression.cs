namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFirstExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    IExpression? PredicateExpression { get; }
}
