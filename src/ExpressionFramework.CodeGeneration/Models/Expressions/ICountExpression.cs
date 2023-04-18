namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ICountExpression : IExpression, ITypedExpression<int>
{
    [Required]
    IExpression Expression { get; }
    IExpression? PredicateExpression { get; }
}
