namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISingleExpression : IExpression
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    ITypedDelegateExpression<bool>? PredicateExpression { get; }
}
