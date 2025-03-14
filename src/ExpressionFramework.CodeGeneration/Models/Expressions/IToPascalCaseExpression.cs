namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Converts the expression to pascal case")]
public interface IToPascalCaseExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject][Description("String to get the pascal case for")] ITypedExpression<string> Expression { get; }
    [Description("Optional CultureInfo to use")] ITypedExpression<CultureInfo>? Culture { get; }
}
