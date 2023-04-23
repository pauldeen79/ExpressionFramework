namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISingleExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    ITypedDelegateExpression<bool>? PredicateExpression { get; }
}
