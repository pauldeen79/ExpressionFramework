namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IDayExpression : IExpression, ITypedExpression<int>
{
    [Required][ValidateObject] ITypedExpression<DateTime> Expression { get; }
}
