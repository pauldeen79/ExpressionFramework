namespace ExpressionFramework.Domain;

public partial record Aggregator
{
    public Result<object?> Aggregate(Expression firstExpression, Expression secondExpression)
        => Aggregate(null, firstExpression, secondExpression);

    public abstract Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression);
}
