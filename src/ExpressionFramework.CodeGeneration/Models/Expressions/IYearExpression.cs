namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IYearExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
}
