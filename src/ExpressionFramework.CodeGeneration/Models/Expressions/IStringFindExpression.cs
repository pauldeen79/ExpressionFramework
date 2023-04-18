namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringFindExpression : IExpression, ITypedExpression<int>
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression FindExpression { get; }
}
