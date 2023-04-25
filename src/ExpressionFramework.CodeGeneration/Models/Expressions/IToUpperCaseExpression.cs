namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IToUpperCaseExpression : IExpression, ITypedExpression<string>
{
    [Required]
    ITypedExpression<string> Expression { get; }
}
