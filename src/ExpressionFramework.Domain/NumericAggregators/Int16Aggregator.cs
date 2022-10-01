namespace ExpressionFramework.Domain.NumericAggregators;

public class Int16Aggregator : INumericAggregator<short>
{
    public Result<object?> Aggregate(object? context, Expression secondExpression, Func<short, short, object> aggregatorDelegate)
    {
        if (context is not short s1)
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
            var s2 = Convert.ToInt16(secondExpressionResult.Value!, CultureInfo.InvariantCulture);
            return Result<object?>.Success(aggregatorDelegate.Invoke(s1, s2));
        }
        catch (Exception ex)
        {
            return Result<object?>.Invalid($"Could not convert SecondExpression to Int16. Error message: {ex.Message}");
        }
    }
}
