namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IMonthExpression : IExpression, ITypedExpression<int>
{
    [Required]
    IExpression Expression { get; }
}
