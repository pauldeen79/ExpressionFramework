namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IDayExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
}
