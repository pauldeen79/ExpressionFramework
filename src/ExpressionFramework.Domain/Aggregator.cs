namespace ExpressionFramework.Domain;

public partial record Aggregator
{
    public Result<object?> Aggregate(Expression firstExpression, Expression secondExpression, ITypedExpression<IFormatProvider>? formatProviderExpression = null)
        => Aggregate(null, firstExpression, secondExpression, formatProviderExpression);

    public abstract Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression, ITypedExpression<IFormatProvider>? formatProviderExpression);
}
