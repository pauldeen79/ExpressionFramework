namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IYearExpression : IExpression, ITypedExpression<int>
{
    [Required]
    ITypedExpression<DateTime> Expression { get; }
}
