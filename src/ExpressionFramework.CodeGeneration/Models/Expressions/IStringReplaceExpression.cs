namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringReplaceExpression : IExpression, ITypedExpression<string>
{
    [Required]
    ITypedExpression<string> Expression { get; }
    [Required]
    ITypedExpression<string> FindExpression { get; }
    [Required]
    ITypedExpression<string> ReplaceExpression { get; }
}
