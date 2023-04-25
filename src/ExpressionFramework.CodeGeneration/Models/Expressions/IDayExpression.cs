namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IDayExpression : IExpression, ITypedExpression<int>
{
    [Required]
    ITypedExpression<DateTime> Expression { get; }
}
