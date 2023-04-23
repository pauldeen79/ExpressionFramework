namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFirstOrDefaultExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    ITypedDelegateExpression<bool>? PredicateExpression { get; }
    IExpression? DefaultExpression { get; }
}
