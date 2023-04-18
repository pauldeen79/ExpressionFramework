namespace ExpressionFramework.Domain.Builders;

//public static class TypedExpressionBuilderFactory
public static partial class ExpressionBuilderFactory
{
    public static ITypedExpressionBuilder<T> CreateTyped<T>(ITypedExpression<T> source)
        => source switch
        {
            TypedConstantExpression<T> x => new TypedConstantExpressionBuilder<T>(x),
            TypedDelegateExpression<T> x => new TypedDelegateExpressionBuilder<T>(x),
            _ => CreateStandard(source)
        };

    private static ITypedExpressionBuilder<T> CreateStandard<T>(ITypedExpression<T> source)
    {
        var registration = registeredTypes.Select(kvp => new { kvp.Key, kvp.Value }).FirstOrDefault(keyValuePair => typeof(ITypedExpression<T>).IsAssignableFrom(keyValuePair.Key));
        return registration == null
            ? throw new NotSupportedException($"Expression of type [{source.GetType()}] is not supported")
            : (ITypedExpressionBuilder<T>)registration.Value.Invoke((Expression)source);
    }
}
