namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IElementAtOrDefaultExpression : IExpression
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    [Required]
    ITypedExpression<int> IndexExpression { get; }
    IExpression? DefaultExpression { get; }
}
