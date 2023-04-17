namespace ExpressionFramework.Domain.Builders.Expressions;

public partial class TypedConstantExpressionBuilder<T> : ITypedExpressionBuilder<T>
{
    ITypedExpression<T> ITypedExpressionBuilder<T>.Build() => BuildTyped();
}
