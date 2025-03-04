namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Gets a number of characters from the specified position of a string value of the specified expression")]
public interface ISubstringExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject][Description("String to get characters from")] ITypedExpression<string> Expression { get; }
    [Required][ValidateObject][Description("Zero-based start position of the characters to return")] ITypedExpression<int> IndexExpression { get; }
    [ValidateObject][Description("Number of characters to use")] ITypedExpression<int>? LengthExpression { get; }
}
