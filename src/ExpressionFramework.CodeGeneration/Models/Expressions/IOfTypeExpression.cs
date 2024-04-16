namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOfTypeExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject] ITypedExpression<Type> TypeExpression { get; }
}
