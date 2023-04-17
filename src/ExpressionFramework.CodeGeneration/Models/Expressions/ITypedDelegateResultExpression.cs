namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITypedDelegateResultExpression<T> : IExpression
{
    [Required]
    Func<object?, Result<T>> Value { get; }
}
