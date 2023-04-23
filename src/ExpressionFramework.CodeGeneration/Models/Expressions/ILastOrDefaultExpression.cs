namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ILastOrDefaultExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    ITypedDelegateExpression<bool>? PredicateExpression { get; }
    IExpression? DefaultExpression { get; }
}
