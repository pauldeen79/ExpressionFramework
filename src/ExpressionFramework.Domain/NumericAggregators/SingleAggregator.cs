﻿namespace ExpressionFramework.Domain.NumericAggregators;

public class SingleAggregator : INumericAggregator<float>
{
    public Result<object?> Aggregate(object? context, Expression secondExpression, Func<float, float, object> aggregatorDelegate)
    {
        if (context is not float f1)
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
            var f2 = Convert.ToSingle(secondExpressionResult.Value!, CultureInfo.InvariantCulture);
            return Result<object?>.Success(aggregatorDelegate.Invoke(f1, f2));
        }
        catch (Exception ex)
        {
            return Result<object?>.Invalid($"Could not convert SecondExpression to Single. Error message: {ex.Message}");
        }
    }
}