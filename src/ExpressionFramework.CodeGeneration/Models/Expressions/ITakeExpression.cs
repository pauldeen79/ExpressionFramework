namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Takes a number of items from an enumerable expression")]
public interface ITakeExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject][Description("Number of items to take")] ITypedExpression<int> CountExpression { get; }
}
