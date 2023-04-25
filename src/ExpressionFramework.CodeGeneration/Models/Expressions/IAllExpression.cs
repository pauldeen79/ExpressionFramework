namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAllExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    [Required]
    ITypedDelegateExpression<bool> PredicateExpression { get; }
}
