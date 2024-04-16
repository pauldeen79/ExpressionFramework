namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISelectExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject] IExpression SelectorExpression { get; }
}
