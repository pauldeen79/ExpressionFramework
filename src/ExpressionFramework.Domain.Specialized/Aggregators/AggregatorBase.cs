namespace ExpressionFramework.Domain.Aggregators;

public static class AggregatorBase
{
    public static Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression, IFormatProvider formatProvider, Func<object?, object?, IFormatProvider, Result<object?>> aggregateDelegate)
    {
        var firstExpressionResult = firstExpression.Evaluate(context);
        if (!firstExpressionResult.IsSuccessful())
        {
            return firstExpressionResult;
        }

        var secondExpressionResult = secondExpression.Evaluate(context);
        if (!secondExpressionResult.IsSuccessful())
        {
            return secondExpressionResult;
        }

        return aggregateDelegate.Invoke(firstExpressionResult.Value, secondExpressionResult.Value, formatProvider);
    }
}
