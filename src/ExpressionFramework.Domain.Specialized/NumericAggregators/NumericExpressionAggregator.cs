namespace ExpressionFramework.Domain.NumericAggregators;

internal static class NumericExpressionAggregator
{
#pragma warning disable S107 // Methods should not have too many parameters
    internal static Result<object?> Evaluate(object? context,
                                             Expression firstExpression,
                                             Expression secondExpression,
                                             IFormatProvider formatProvider,
                                             Func<byte, byte, object> byteAggregatorDelegate,
                                             Func<short, short, object> shortAggregatorDelegate,
                                             Func<int, int, object> intAggregatorDelegate,
                                             Func<long, long, object> longAggregatorDelegate,
                                             Func<float, float, object> singleAggregatorDelegate,
                                             Func<decimal, decimal, object> decimalAggregatorDelegate,
                                             Func<double, double, object> doubleAggregatorDelegate)
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

        return NumericAggregator.Evaluate(firstExpressionResult.Value,
                                          secondExpressionResult.Value,
                                          formatProvider,
                                          byteAggregatorDelegate,
                                          shortAggregatorDelegate,
                                          intAggregatorDelegate,
                                          longAggregatorDelegate,
                                          singleAggregatorDelegate,
                                          decimalAggregatorDelegate,
                                          doubleAggregatorDelegate);
    }
}
