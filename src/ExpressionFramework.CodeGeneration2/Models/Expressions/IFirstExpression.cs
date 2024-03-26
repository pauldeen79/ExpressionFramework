namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFirstExpression : IExpression
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject] ITypedExpression<bool>? PredicateExpression { get; }
}
