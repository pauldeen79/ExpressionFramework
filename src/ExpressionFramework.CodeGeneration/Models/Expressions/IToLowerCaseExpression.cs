namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Converts the expression to lower case")]
public interface IToLowerCaseExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject][Description("String to get the lower case for")] ITypedExpression<string> Expression { get; }
    [Description("Optional CultureInfo to use")] ITypedExpression<CultureInfo>? Culture { get; }
}
