namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAllExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression PredicateExpression { get; }
}
