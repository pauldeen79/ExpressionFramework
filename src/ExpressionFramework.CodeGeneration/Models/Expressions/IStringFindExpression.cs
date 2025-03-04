namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns the position of the find expression within the (string) expression")]
public interface IStringFindExpression : IExpression, ITypedExpression<int>
{
    [Required][ValidateObject][Description("String to find text in")] ITypedExpression<string> Expression { get; }
    [Required][ValidateObject][Description("String to find")] ITypedExpression<string> FindExpression { get; }
}
