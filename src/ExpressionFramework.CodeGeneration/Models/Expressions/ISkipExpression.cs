namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISkipExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression CountExpression { get; }
}
