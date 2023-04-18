namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringReplaceExpression : IExpression, ITypedExpression<string>
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression FindExpression { get; }
    [Required]
    IExpression ReplaceExpression { get; }
}
