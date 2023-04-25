namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IMonthExpression : IExpression, ITypedExpression<int>
{
    [Required]
    ITypedExpression<DateTime> Expression { get; }
}
