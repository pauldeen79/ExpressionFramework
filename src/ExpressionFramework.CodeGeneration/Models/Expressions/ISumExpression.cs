namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISumExpression : IExpression
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    IExpression? SelectorExpression { get; }
}
