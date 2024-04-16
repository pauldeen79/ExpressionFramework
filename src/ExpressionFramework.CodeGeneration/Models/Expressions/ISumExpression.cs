namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISumExpression : IExpression
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject] IExpression? SelectorExpression { get; }
}
