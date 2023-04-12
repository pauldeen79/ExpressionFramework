namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IMonthExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
}
