namespace ExpressionFramework.Domain.Models;

public static partial class ExpressionModelFactory
{
    public static ITypedExpressionModel<T> CreateTyped<T>(ITypedExpression<T> source)
        => source switch
        {
            TypedConstantExpression<T> x => new TypedConstantExpressionModel<T>(x),
            TypedConstantResultExpression<T> x => new TypedConstantResultExpressionModel<T>(x),
            TypedDelegateExpression<T> x => new TypedDelegateExpressionModel<T>(x),
            TypedDelegateResultExpression<T> x => new TypedDelegateResultExpressionModel<T>(x),
            _ => CreateStandard(source)
        };

    private static ITypedExpressionModel<T> CreateStandard<T>(ITypedExpression<T> source)
    {
        var registration = registeredTypes
            .Select(kvp => new { kvp.Key, kvp.Value })
            .FirstOrDefault(keyValuePair => typeof(ITypedExpression<T>).IsAssignableFrom(keyValuePair.Key) && source.GetType().IsAssignableFrom(keyValuePair.Key));
        
        return registration is null
            ? throw new NotSupportedException($"Expression of type [{source.GetType()}] is not supported")
            : (ITypedExpressionModel<T>)registration.Value.Invoke((Expression)source);
    }
}
