namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Converts the expression to camel case")]
public interface IToCamelCaseExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject][Description("String to get the camel case for")] ITypedExpression<string> Expression { get; }
    [Description("Optional CultureInfo to use")] ITypedExpression<CultureInfo>? Culture { get; }
}
