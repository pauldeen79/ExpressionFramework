namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Gets the value at the specified index from the (enumerable) expression, or default value when not found")]
public interface IElementAtOrDefaultExpression : IExpression
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject][Description("Index of the item to get")] ITypedExpression<int> IndexExpression { get; }
    [ValidateObject][Description("Expression to use in case the index could not be found")] IExpression? DefaultExpression { get; }
}
