namespace ExpressionFramework.Domain.NumericAggregators;

public class ByteAggregator : INumericAggregator<byte>
{
    public Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression, Func<byte, byte, object> aggregatorDelegate)
    {
        var result1 = firstExpression.Evaluate(context);
        if (!result1.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(result1);
        }

        if (result1.Value is not byte b1)
        {
            return Result<object?>.NotSupported();
        }

        var result2 = secondExpression.Evaluate(context);
        if (!result2.IsSuccessful())
        {
            return result2;
        }

        byte b2;
        try
        {
            b2 = Convert.ToByte(result2.Value!, CultureInfo.InvariantCulture);
        }
        catch (Exception ex)
        {
            return Result<object?>.Invalid($"Could not convert SecondExpression to Byte. Error message: {ex.Message}");
        }

        try
        {
            return Result<object?>.Success(aggregatorDelegate.Invoke(b1, b2));
        }
        catch (Exception ex)
        {
            return Result<object?>.Error($"Aggregation failed. Error message: {ex.Message}");
        }
    }
}
