namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Gets the number of items from the (enumerable) expression, optionally using a predicate")]
public interface ICountExpression : IExpression, ITypedExpression<int>
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject][Description("Optional predicate to use")] ITypedExpression<bool>? PredicateExpression { get; }
}
