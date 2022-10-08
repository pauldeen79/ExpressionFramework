namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IWhereExpression : IExpression
{
    [Required]
    IExpression PredicateExpression { get; }
}
