namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFirstExpression : IExpression
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    ITypedDelegateExpression<bool>? PredicateExpression { get; }
}
