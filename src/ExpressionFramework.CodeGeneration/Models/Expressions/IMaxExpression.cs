namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IMaxExpression : IExpression
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    IExpression? SelectorExpression { get; }
}
