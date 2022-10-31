namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface INotExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
}
