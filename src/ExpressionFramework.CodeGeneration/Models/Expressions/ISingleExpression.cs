namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Gets a single value from the (enumerable) expression, optionally using a predicate to select an item")]
public interface ISingleExpression : IExpression
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject][Description("Optional predicate to use")] ITypedExpression<bool>? PredicateExpression { get; }
}
