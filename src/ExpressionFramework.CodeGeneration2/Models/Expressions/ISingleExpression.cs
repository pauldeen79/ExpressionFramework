namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISingleExpression : IExpression
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject] ITypedExpression<bool>? PredicateExpression { get; }
}
