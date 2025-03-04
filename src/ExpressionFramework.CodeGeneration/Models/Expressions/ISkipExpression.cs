namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Skips a number of items on an enumerable expression")]
public interface ISkipExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject][Description("Number of items to skip")] ITypedExpression<int> CountExpression { get; }
}
