namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IWhereExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject] ITypedExpression<bool> PredicateExpression { get; }
}
