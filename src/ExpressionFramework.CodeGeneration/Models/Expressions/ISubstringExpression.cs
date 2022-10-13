namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISubstringExpression : IExpression
{
    [Required]
    IExpression IndexExpression { get; }
    [Required]
    IExpression LengthExpression { get; }
}
