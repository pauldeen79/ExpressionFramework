namespace ExpressionFramework.Domain;

public static class TypedExpressionExtensions
{
    public static Result<T> EvaluateTyped<T>(this ITypedExpression<T> instance)
        => instance.EvaluateTyped(null);

    public static ITypedExpressionBuilder<T> ToBuilder<T>(this ITypedExpression<T> source)
        => source switch
        {
            TypedConstantExpression<T> x => new TypedConstantExpressionBuilder<T>(x),
            TypedConstantResultExpression<T> x => new TypedConstantResultExpressionBuilder<T>(x),
            TypedDelegateExpression<T> x => new TypedDelegateExpressionBuilder<T>(x),
            TypedDelegateResultExpression<T> x => new TypedDelegateResultExpressionBuilder<T>(x),
            Expression e => (ITypedExpressionBuilder<T>)e.ToBuilder(),
            _ => throw new NotSupportedException("Typed expression should be inherited from Expression")
        };
}
