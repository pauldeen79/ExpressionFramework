namespace ExpressionFramework.Domain.Builders.Expressions;

public partial class TypedDelegateExpressionBuilder<T>
{
    public static implicit operator TypedDelegateExpressionBuilder<T>(Func<object?, T> value) => new TypedDelegateExpressionBuilder<T>().WithValue(value);
}
