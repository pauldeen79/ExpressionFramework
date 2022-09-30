namespace ExpressionFramework.Domain;

public interface INumericAggregator<T>
{
    Result<object?> Aggregate(object? context, Expression secondExpression, Func<T, T, object> aggregatorDelegate);
}
