namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IToLowerCaseExpression : IExpression, ITypedExpression<string>
{
    [Required]
    ITypedExpression<string> Expression { get; }
}
