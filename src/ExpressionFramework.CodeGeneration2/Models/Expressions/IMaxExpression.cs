namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IMaxExpression : IExpression
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject] IExpression? SelectorExpression { get; }
}
