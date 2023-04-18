namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IWhereExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression PredicateExpression { get; }
}
