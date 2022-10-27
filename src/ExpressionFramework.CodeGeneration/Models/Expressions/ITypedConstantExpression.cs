namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITypedConstantExpression<out T> : IExpression
{
    T Value { get; }
}
