namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOrExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
}
