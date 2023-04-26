namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ILastExpression : IExpression
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    ITypedExpression<bool>? PredicateExpression { get; }
}
