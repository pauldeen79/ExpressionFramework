namespace ExpressionFramework.Domain.NumericAggregators;

public class Int64Aggregator : INumericAggregator<long>
{
    public Result<object?> Aggregate(object? context, Expression secondExpression, Func<long, long, object> aggregatorDelegate)
    {
        if (context is not long l1)
        {
            return Result<object?>.NotSupported();
        }
        
        var secondExpressionResult = secondExpression.Evaluate(context);
        if (!secondExpressionResult.IsSuccessful())
        {
            return secondExpressionResult;
        }

        try
        {
            var l2 = Convert.ToInt64(secondExpressionResult.Value!, CultureInfo.InvariantCulture);
            return Result<object?>.Success(aggregatorDelegate.Invoke(l1, l2));
        }
        catch (Exception ex)
        {
            return Result<object?>.Invalid($"Could not convert SecondExpression to Int64. Error message: {ex.Message}");
        }
    }
}
