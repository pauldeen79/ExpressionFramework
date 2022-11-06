namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringReplaceExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression FindExpression { get; }
    [Required]
    IExpression ReplaceExpression { get; }
}
