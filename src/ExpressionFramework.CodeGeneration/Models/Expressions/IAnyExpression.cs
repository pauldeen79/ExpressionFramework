namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns an indicator whether any item from the (enumerable) expression conform to the optional predicate")]
public interface IAnyExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject][Description("Optional predicate to use")] ITypedExpression<bool>? PredicateExpression { get; }
}
