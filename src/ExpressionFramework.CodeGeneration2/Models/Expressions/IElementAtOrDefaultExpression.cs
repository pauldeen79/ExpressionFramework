namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IElementAtOrDefaultExpression : IExpression
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject] ITypedExpression<int> IndexExpression { get; }
    [ValidateObject] IExpression? DefaultExpression { get; }
}
