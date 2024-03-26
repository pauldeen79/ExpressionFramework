namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITypedConstantResultExpression<T> : IExpression, ITypedExpression<T>
{
    [Required]Result<T> Value { get; }
}
