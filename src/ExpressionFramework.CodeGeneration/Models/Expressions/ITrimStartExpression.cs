namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Trims the start characters of the expression")]
public interface ITrimStartExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject][Description("String to get the trimmed value for")] ITypedExpression<string> Expression { get; }
    [ValidateObject][Description("Optional trim characters, or space when empty")] ITypedExpression<char[]>? TrimCharsExpression { get; }
}
