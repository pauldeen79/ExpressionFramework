namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns the month from the specified DateTime expression")]
public interface IMonthExpression : IExpression, ITypedExpression<int>
{
    [Required][ValidateObject][Description("DateTime source expression")] ITypedExpression<DateTime> Expression { get; }
}
