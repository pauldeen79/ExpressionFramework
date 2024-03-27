namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IMinExpression : IExpression
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject] IExpression? SelectorExpression { get; }
}
