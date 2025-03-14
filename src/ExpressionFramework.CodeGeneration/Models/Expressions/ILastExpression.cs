namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Gets the last value from the (enumerable) expression, optionally using a predicate to select an item")]
public interface ILastExpression : IExpression
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject][Description("Predicate to use")] ITypedExpression<bool>? PredicateExpression { get; }
}
