namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFirstOrDefaultExpression : IExpression
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject] ITypedExpression<bool>? PredicateExpression { get; }
    [ValidateObject] IExpression? DefaultExpression { get; }
}
