namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Groups items from an enumerable expression using a key selector expression")]
public interface IGroupByExpression : IExpression
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject][Description("Expression to use on each item to select the key")] IExpression KeySelectorExpression { get; }
}
