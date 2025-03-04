namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Transforms items from an enumerable expression using an expression")]
public interface ISelectExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject][Description("Selector expression to use on each item")] IExpression SelectorExpression { get; }
}
