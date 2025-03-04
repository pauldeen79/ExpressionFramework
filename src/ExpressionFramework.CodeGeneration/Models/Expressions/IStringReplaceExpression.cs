namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns the position of the find expression within the (string) expression")]
public interface IStringReplaceExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject][Description("String to find text in")] ITypedExpression<string> Expression { get; }
    [Required][ValidateObject][Description("String to find")] ITypedExpression<string> FindExpression { get; }
    [Required][ValidateObject][Description("String to replace the found text with")] ITypedExpression<string> ReplaceExpression { get; }
}
