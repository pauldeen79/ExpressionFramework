namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Filters an enumerable expression on type")]
public interface IOfTypeExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required][ValidateObject][Description("Enumerable expression to use")] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject][Description("Type to filter on")] ITypedExpression <Type> TypeExpression { get; }
}
