namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Gets a single value from the (enumerable) expression, optionally using a predicate to select an item, or default value when not found")]
public interface ISingleOrDefaultExpression : IExpression
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject][Description("Optional predicate to use")] ITypedExpression<bool>? PredicateExpression { get; }
    [ValidateObject][Description("Expression to use in case no items of the enumerable expression match the predicate")] IExpression? DefaultExpression { get; }
}
