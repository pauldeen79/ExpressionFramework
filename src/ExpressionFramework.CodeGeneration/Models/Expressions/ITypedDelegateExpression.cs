namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITypedDelegateExpression<T> : IExpression
{
    [Required]
    Func<object?, T> Value { get; }
}

