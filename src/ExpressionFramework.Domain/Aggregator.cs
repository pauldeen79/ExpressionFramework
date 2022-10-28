namespace ExpressionFramework.Domain;

public partial record Aggregator
{
    public abstract Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression);
}
