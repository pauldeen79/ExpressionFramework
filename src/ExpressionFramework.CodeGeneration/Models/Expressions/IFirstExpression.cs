namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Gets the first value from the (enumerable) expression, optionally using a predicate to select an item")]
public interface IFirstExpression : IExpression
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject][Description("Optional predicate to use")] ITypedExpression<bool>? PredicateExpression { get; }
}
