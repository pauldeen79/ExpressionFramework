namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns the OR-combination value of two boolean expressions")]
public interface IOrExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject][Description("First boolean expression to perform OR combination on")] ITypedExpression<bool> FirstExpression { get; }
    [Required][ValidateObject][Description("Second boolean expression to perform OR combination on")] ITypedExpression<bool> SecondExpression { get; }
}
