namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface INotEqualsExpression : IExpression
{
    [Required]
    IExpression FirstExpression { get; }
    [Required]
    IExpression SecondExpression { get; }
}
