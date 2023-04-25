namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IWhereExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    [Required]
    ITypedDelegateExpression<bool> PredicateExpression { get; }
}
