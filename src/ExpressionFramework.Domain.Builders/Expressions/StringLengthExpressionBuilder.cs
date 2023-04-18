namespace ExpressionFramework.Domain.Builders.Expressions;

public partial class StringLengthExpressionBuilder : ITypedExpressionBuilder<int>
{
    ITypedExpression<int> ITypedExpressionBuilder<int>.Build() => BuildTyped();
}
