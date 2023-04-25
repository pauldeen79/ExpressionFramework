namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IMinExpression : IExpression
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    IExpression? SelectorExpression { get; }
}
