namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ILastOrDefaultExpression : IExpression
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    ITypedExpression<bool>? PredicateExpression { get; }
    IExpression? DefaultExpression { get; }
}
