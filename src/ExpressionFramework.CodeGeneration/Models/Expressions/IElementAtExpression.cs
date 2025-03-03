namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Gets the value at the specified index from the (enumerable) expression")]
public interface IElementAtExpression : IExpression
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject][Description("Index of the item to get")] ITypedExpression<int> IndexExpression { get; }
}
