namespace ExpressionFramework.Domain.Builders.Expressions;

public partial class TypedConstantExpressionBuilder<T>
{
    partial void SetDefaultValues()
    {
        if (typeof(T) == typeof(string))
        {
            GetType().GetProperty(nameof(Value)).SetValue(this, string.Empty);
        }
    }
}
