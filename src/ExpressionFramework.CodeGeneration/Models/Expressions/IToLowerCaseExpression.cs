namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IToLowerCaseExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
}
