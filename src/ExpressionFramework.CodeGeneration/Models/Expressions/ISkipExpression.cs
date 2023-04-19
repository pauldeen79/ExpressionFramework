namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISkipExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    [Required]
    ITypedExpression<int> CountExpression { get; }
}
