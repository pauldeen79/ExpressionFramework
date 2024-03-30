namespace ExpressionFramework.Domain.Contracts;

public interface ITypedExpressionBuilder<T>
{
    ITypedExpression<T> BuildAsTypedExpression();
}
