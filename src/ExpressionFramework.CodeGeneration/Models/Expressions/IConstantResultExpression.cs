namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IConstantResultExpression : IExpression
{
    [Required]
    Result Value { get; }
}
