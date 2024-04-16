namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IMonthExpression : IExpression, ITypedExpression<int>
{
    [Required][ValidateObject] ITypedExpression<DateTime> Expression { get; }
}
