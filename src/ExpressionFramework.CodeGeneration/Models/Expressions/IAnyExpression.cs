namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAnyExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    ITypedExpression<bool>? PredicateExpression { get; }
}
