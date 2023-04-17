namespace ExpressionFramework.Domain.Builders.Expressions;

public partial class TypedDelegateExpressionBuilder<T> : ITypedExpressionBuilder<T>
{
    ITypedExpression<T> ITypedExpressionBuilder<T>.Build() => BuildTyped();
}
