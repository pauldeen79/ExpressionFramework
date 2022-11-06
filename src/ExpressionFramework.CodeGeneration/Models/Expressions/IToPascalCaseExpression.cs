namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IToPascalCaseExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
}
