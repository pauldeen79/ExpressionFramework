namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAllExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject] ITypedExpression<bool> PredicateExpression { get; }
}
