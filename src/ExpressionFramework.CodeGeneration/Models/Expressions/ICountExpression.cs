namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ICountExpression : IExpression, ITypedExpression<int>
{
    [Required]
    ITypedExpression<IEnumerable> Expression { get; }
    ITypedExpression<bool>? PredicateExpression { get; }
}
