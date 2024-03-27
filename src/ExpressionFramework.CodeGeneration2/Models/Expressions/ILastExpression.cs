namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ILastExpression : IExpression
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject] ITypedExpression<bool>? PredicateExpression { get; }
}
