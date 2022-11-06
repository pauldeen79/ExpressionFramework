namespace ExpressionFramework.Domain.NumericAggregators;

public class DoubleAggregator : INumericAggregator<double>
{
    public Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression, Func<double, double, object> aggregatorDelegate)
    {
        var result1 = firstExpression.Evaluate(context);
        if (!result1.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(result1);
        }

        if (result1.Value is not double d1)
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
            var d2 = Convert.ToDouble(secondExpressionResult.Value!, CultureInfo.InvariantCulture);
            return Result<object?>.Success(aggregatorDelegate.Invoke(d1, d2));
        }
        catch (Exception ex)
        {
            return Result<object?>.Invalid($"Could not convert SecondExpression to Double. Error message: {ex.Message}");
        }
    }
}
