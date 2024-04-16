namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ICountExpression : IExpression, ITypedExpression<int>
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject] ITypedExpression<bool>? PredicateExpression { get; }
}
