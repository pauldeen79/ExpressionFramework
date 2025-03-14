namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns the AND-combination value of two boolean expressions")]
public interface IAndExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject][Description("First boolean expression to perform AND combination on")] ITypedExpression<bool> FirstExpression { get; }
    [Required][ValidateObject][Description("Second boolean expression to perform AND combination on")] ITypedExpression<bool> SecondExpression { get; }
}
