namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ILeftExpression : IExpression
{
    [Required]
    IExpression LengthExpression { get; }
}
