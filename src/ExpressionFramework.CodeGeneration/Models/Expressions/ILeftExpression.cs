namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Gets a number of characters of the start of a string value of the specified expression")]
public interface ILeftExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject][Description("String to get the first characters for")] ITypedExpression<string> Expression { get; }
    [Required][ValidateObject][Description("Number of characters to get")] ITypedExpression<int> LengthExpression { get; }
}
