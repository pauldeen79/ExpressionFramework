namespace ExpressionFramework.Domain.NumericAggregators;

public class DecimalAggregator : INumericAggregator<decimal>
{
    public Result<object?> Aggregate(object? context, Expression secondExpression, Func<decimal, decimal, object> aggregatorDelegate)
    {
        if (context is not decimal d1)
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
            var d2 = Convert.ToDecimal(secondExpressionResult.Value!, CultureInfo.InvariantCulture);
            return Result<object?>.Success(aggregatorDelegate.Invoke(d1, d2));
        }
        catch (Exception ex)
        {
            return Result<object?>.Invalid($"Could not convert SecondExpression to Decimal. Error message: {ex.Message}");
        }
    }
}
