namespace ExpressionFramework.Domain.Contracts;

public interface ITypedExpressionModel<T>
{
    ITypedExpression<T> ToEntity();
}
