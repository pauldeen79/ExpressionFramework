namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns the length of the (string) expression")]
public interface IStringLengthExpression : IExpression, ITypedExpression<int>
{
    [Required][ValidateObject][Description("String to get the length for")] ITypedExpression<string> Expression { get; }
}
