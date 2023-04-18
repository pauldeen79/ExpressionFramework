namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISelectExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression SelectorExpression { get; }
}
