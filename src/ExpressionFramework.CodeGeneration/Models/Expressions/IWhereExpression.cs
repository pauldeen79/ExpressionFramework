namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Filters an enumerable expression using a predicate")]
public interface IWhereExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject][Description("Predicate to use")] ITypedExpression<bool> PredicateExpression { get; }
}
