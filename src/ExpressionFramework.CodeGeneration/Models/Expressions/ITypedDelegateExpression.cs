namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITypedDelegateExpression<T> : IExpression, ITypedExpression<T>
{
    [Required]
    Func<object?, T> Value { get; }
}

