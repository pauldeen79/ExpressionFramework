namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAnyExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject] ITypedExpression<bool>? PredicateExpression { get; }
}
