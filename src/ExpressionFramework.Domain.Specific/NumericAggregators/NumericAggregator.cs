namespace ExpressionFramework.Domain.NumericAggregators;

internal static class NumericAggregator
{
#pragma warning disable S107 // Methods should not have too many parameters
    internal static Result<object?> Evaluate(object? context,
                                             Expression firstExpression,
                                             Expression secondExpression,
                                             Func<byte, byte, object> byteAggregatorDelegate,
                                             Func<short, short, object> shortAggregatorDelegate,
                                             Func<int, int, object> intAggregatorDelegate,
                                             Func<long, long, object> longAggregatorDelegate,
                                             Func<float, float, object> singleAggregatorDelegate,
                                             Func<decimal, decimal, object> decimalAggregatorDelegate,
                                             Func<double, double, object> doubleAggregatorDelegate)
#pragma warning restore S107 // Methods should not have too many parameters
        => new Func<Result<object?>>[]
        {
            () => new ByteAggregator().Aggregate(context, firstExpression, secondExpression, byteAggregatorDelegate),
            () => new Int16Aggregator().Aggregate(context, firstExpression, secondExpression, shortAggregatorDelegate),
            () => new Int32Aggregator().Aggregate(context, firstExpression, secondExpression, intAggregatorDelegate),
            () => new Int64Aggregator().Aggregate(context, firstExpression, secondExpression, longAggregatorDelegate),
            () => new SingleAggregator().Aggregate(context, firstExpression, secondExpression, singleAggregatorDelegate),
            () => new DoubleAggregator().Aggregate(context, firstExpression, secondExpression, doubleAggregatorDelegate),
            () => new DecimalAggregator().Aggregate(context, firstExpression, secondExpression, decimalAggregatorDelegate),
            () => Result<object?>.Invalid("First expression is not of a supported type")
        }.Select(x => x.Invoke()).First(x => x.Status != ResultStatus.NotSupported);
}
