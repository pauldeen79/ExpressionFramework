namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ICountExpression : IExpression, ITypedExpression<int>
{
    [Required]
    IExpression Expression { get; }
    ITypedDelegateExpression<bool>? PredicateExpression { get; }
}
