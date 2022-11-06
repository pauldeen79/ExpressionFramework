namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAndExpression : IExpression
{
    [Required]
    IExpression FirstExpression { get; }
    [Required]
    IExpression SecondExpression { get; }
}
