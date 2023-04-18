namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITypedDelegateResultExpression<T> : IExpression, ITypedExpression<Result<T>>
{
    [Required]
    Func<object?, Result<T>> Value { get; }
}
