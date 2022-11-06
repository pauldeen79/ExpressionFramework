namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringLengthExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
}
