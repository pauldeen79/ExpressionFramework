namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Converts the expression to upper case")]
public interface IToUpperCaseExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject][Description("String to get the upper case for")] ITypedExpression<string> Expression { get; }
    [Description("Optional CultureInfo to use")] ITypedExpression<CultureInfo>? Culture { get; }
}
