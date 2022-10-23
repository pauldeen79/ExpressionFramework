namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAndExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
}
