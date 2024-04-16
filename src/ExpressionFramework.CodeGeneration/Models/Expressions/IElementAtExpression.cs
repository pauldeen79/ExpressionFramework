namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IElementAtExpression : IExpression
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject] ITypedExpression<int> IndexExpression { get; }
}
