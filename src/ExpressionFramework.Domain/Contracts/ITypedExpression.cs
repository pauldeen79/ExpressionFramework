namespace ExpressionFramework.Domain.Contracts;

public interface ITypedExpression<T> : IUntypedExpressionProvider, IExpression
{
    Result<T> EvaluateTyped(object? context);
    ITypedExpressionBuilder<T> ToTypedBuilder();
}
