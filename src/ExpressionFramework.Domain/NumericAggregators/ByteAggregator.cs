namespace ExpressionFramework.Domain.NumericAggregators;

public class ByteAggregator : INumericAggregator<byte>
{
    public Result<object?> Aggregate(object? context, Expression secondExpression, Func<byte, byte, object> aggregatorDelegate)
    {
        if (context is not byte b1)
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
            var b2 = Convert.ToByte(secondExpressionResult.Value!, CultureInfo.InvariantCulture);
            return Result<object?>.Success(aggregatorDelegate.Invoke(b1, b2));
        }
        catch (Exception ex)
        {
            return Result<object?>.Invalid($"Could not convert SecondExpression to Byte. Error message: {ex.Message}");
        }
    }
}
