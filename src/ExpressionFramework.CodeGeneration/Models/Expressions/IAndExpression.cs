namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns the AND-combination value of two boolean expressions")]
public interface IAndExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject][Description("Boolean expression to perform AND combination on")] ITypedExpression<bool> FirstExpression { get; }
    [Required][ValidateObject][Description("Boolean expression to perform AND combination on")] ITypedExpression<bool> SecondExpression { get; }
}
