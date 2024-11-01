namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IToCamelCaseExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject] ITypedExpression<string> Expression { get; }
    ITypedExpression<CultureInfo>? Culture { get; }
}
