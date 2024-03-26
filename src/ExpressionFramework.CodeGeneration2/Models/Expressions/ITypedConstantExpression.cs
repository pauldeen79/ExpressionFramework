namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITypedConstantExpression<T> : IExpression, ITypedExpression<T>
{
    T Value { get; }
}
