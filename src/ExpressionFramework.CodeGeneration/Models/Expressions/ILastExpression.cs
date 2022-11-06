namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ILastExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    IExpression? PredicateExpression { get; }
}
