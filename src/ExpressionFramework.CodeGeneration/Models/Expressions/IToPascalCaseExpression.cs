namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IToPascalCaseExpression : IExpression, ITypedExpression<string>
{
    [Required]
    ITypedExpression<string> Expression { get; }
}
