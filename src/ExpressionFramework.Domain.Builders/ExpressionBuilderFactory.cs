namespace ExpressionFramework.Domain.Builders;

public static partial class ExpressionBuilderFactory
{
    public static ITypedExpressionBuilder<T> CreateTyped<T>(ITypedExpression<T> source)
        => source switch
        {
            TypedConstantExpression<T> x => new TypedConstantExpressionBuilder<T>(x),
            TypedConstantResultExpression<T> x => new TypedConstantResultExpressionBuilder<T>(x),
            TypedDelegateExpression<T> x => new TypedDelegateExpressionBuilder<T>(x),
            TypedDelegateResultExpression<T> x => new TypedDelegateResultExpressionBuilder<T>(x),
            _ => CreateStandard(source)
        };

    private static ITypedExpressionBuilder<T> CreateStandard<T>(ITypedExpression<T> source)
    {
        var registration = registeredTypes
            .Select(kvp => new { kvp.Key, kvp.Value })
            .FirstOrDefault(keyValuePair => typeof(ITypedExpression<T>).IsAssignableFrom(keyValuePair.Key) && source.GetType().IsAssignableFrom(keyValuePair.Key));
        
        return registration is null
            ? throw new NotSupportedException($"Expression of type [{source.GetType()}] is not supported")
            : (ITypedExpressionBuilder<T>)registration.Value.Invoke((Expression)source);
    }
}
