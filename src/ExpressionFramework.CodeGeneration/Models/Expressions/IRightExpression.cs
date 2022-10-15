namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IRightExpression : IExpression
{
    [Required]
    IExpression LengthExpression { get; }
}
