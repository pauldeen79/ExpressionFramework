namespace ExpressionFramework.Domain.NumericAggregators;

public class Int32Aggregator : INumericAggregator<int>
{
    public Result<object?> Aggregate(object? context, Expression secondExpression, Func<int, int, object> aggregatorDelegate)
    {
        if (context is not int i1)
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
            var i2 = Convert.ToInt32(secondExpressionResult.Value!, CultureInfo.InvariantCulture);
            return Result<object?>.Success(aggregatorDelegate.Invoke(i1, i2));
        }
        catch (Exception ex)
        {
            return Result<object?>.Invalid($"Could not convert SecondExpression to Int32. Error message: {ex.Message}");
        }
    }
}
