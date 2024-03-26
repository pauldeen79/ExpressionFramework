namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IToUpperCaseExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject] ITypedExpression<string> Expression { get; }
}
