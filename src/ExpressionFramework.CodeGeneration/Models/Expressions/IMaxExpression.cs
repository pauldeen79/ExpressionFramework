namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Gets the maximum value from the (enumerable) expression, optionally using a selector expression")]
public interface IMaxExpression : IExpression
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [ValidateObject][Description("Optional selection expression")] IExpression? SelectorExpression { get; }
}
