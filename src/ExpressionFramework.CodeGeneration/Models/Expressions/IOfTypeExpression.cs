namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOfTypeExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression TypeExpression { get; }
}
