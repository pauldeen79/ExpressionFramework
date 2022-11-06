namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOrExpression : IExpression
{
    [Required]
    IExpression FirstExpression { get; }
    [Required]
    IExpression SecondExpression { get; }
}
