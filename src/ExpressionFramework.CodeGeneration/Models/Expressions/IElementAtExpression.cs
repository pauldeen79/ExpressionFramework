namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IElementAtExpression : IExpression
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    [Required]
    ITypedExpression<int> IndexExpression { get; }
}
