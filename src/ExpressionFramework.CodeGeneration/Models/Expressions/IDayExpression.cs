namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns the day from the specified DateTime expression")]
public interface IDayExpression : IExpression, ITypedExpression<int>
{
    [Required][ValidateObject][Description("DateTime source expression")] ITypedExpression<DateTime> Expression { get; }
}
