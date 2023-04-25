namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISelectExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    [Required]
    IExpression SelectorExpression { get; }
}
