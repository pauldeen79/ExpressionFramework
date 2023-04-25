namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISingleOrDefaultExpression : IExpression
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    ITypedDelegateExpression<bool>? PredicateExpression { get; }
    IExpression? DefaultExpression { get; }
}
