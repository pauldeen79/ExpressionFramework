namespace ExpressionFramework.Domain.NumericAggregators;

public class Int32Aggregator : INumericAggregator<int>
{
    public Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression, Func<int, int, object> aggregatorDelegate)
    {
        var result1 = firstExpression.Evaluate(context);
        if (!result1.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(result1);
        }

        if (result1.Value is not int i1)
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
