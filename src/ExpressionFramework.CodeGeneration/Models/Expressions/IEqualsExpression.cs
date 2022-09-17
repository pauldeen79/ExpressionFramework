namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IEqualsExpression : IExpression
{
    [Required]
    IExpression FirstExpression { get; }
    [Required]
    IExpression SecondExpression { get; }
}
