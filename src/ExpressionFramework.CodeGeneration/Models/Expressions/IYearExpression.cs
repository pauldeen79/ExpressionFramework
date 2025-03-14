namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns the year from the specified DateTime expression")]
public interface IYearExpression : IExpression, ITypedExpression<int>
{
    [Required][ValidateObject][Description("DateTime source expression")] ITypedExpression<DateTime> Expression { get; }
}
