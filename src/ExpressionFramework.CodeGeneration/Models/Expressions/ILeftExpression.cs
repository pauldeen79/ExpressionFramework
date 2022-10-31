namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ILeftExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression LengthExpression { get; }
}
