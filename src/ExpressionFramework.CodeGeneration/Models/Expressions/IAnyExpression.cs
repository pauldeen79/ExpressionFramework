namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAnyExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    IExpression Expression { get; }
    IExpression? PredicateExpression { get; }
}
