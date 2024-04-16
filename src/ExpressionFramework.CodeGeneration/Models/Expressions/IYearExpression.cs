namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IYearExpression : IExpression, ITypedExpression<int>
{
    [Required][ValidateObject] ITypedExpression<DateTime> Expression { get; }
}
