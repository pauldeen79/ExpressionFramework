namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IToUpperCaseExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
}
