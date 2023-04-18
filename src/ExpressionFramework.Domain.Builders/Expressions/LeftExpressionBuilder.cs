namespace ExpressionFramework.Domain.Builders.Expressions;

public partial class LeftExpressionBuilder : ITypedExpressionBuilder<string>
{
    ITypedExpression<string> ITypedExpressionBuilder<string>.Build() => BuildTyped();
}
