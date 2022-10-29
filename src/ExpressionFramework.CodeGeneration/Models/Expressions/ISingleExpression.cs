namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISingleExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    IExpression? PredicateExpression { get; }
}
