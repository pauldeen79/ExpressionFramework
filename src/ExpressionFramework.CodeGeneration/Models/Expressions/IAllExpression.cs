namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns an indicator whether all items from the (enumerable) expression conform to the predicate")]
public interface IAllExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression <IEnumerable> Expression { get; }
    [Required][ValidateObject][Description("Predicate to use")] ITypedExpression<bool> PredicateExpression { get; }
}
