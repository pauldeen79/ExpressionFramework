namespace ExpressionFramework.Domain.Builders.Expressions;

public partial class TypedConstantExpressionBuilder<T>
{
    public static implicit operator TypedConstantExpressionBuilder<T>(T value) => new TypedConstantExpressionBuilder<T>().WithValue(value);
}
