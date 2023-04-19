namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITypedConstantResultExpression<T> : IExpression, ITypedExpression<T>
{
    Result<T> Value { get; }
}
