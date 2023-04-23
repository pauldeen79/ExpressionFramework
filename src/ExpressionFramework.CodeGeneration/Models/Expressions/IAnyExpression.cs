namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAnyExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    IExpression Expression { get; }
    ITypedDelegateExpression<bool>? PredicateExpression { get; }
}
