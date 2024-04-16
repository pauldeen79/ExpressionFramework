namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IToPascalCaseExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject] ITypedExpression<string> Expression { get; }
}
