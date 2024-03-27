namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISkipExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required][ValidateObject] ITypedExpression<IEnumerable> Expression { get; }
    [Required][ValidateObject] ITypedExpression<int> CountExpression { get; }
}
