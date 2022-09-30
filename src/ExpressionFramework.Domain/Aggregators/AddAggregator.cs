namespace ExpressionFramework.Domain.Aggregators;

public partial record AddAggregator
{
    public override Result<object?> Aggregate(object? context, Expression secondExpression)
    {
        var byteAggregatorDelegate = new Func<byte, byte, object>((b1, b2) => b1 + b2);
        var shortAggregatorDelegate = new Func<short, short, object>((s1, s2) => s1 + s2);
        var intAggregatorDelegate = new Func<int, int, object>((i1, i2) => i1 + i2);
        var longAggregatorDelegate = new Func<long, long, object>((l1, l2) => l1 + l2);
        var singleAggregatorDelegate = new Func<float, float, object>((f1, f2) => f1 + f2);
        var decimalAggregatorDelegate = new Func<decimal, decimal, object>((d1, d2) => d1 + d2);
        var doubleAggregatorDelegate = new Func<double, double, object>((d1, d2) => d1 + d2);

        var delegates = new Func<Result<object?>>[]
        {
            () => new ByteAggregator().Aggregate(context, secondExpression, byteAggregatorDelegate),
            () => new Int16Aggregator().Aggregate(context, secondExpression, shortAggregatorDelegate),
            () => new Int32Aggregator().Aggregate(context, secondExpression, intAggregatorDelegate),
            () => new Int64Aggregator().Aggregate(context, secondExpression, longAggregatorDelegate),
            () => new SingleAggregator().Aggregate(context, secondExpression, singleAggregatorDelegate),
            () => new DoubleAggregator().Aggregate(context, secondExpression, doubleAggregatorDelegate),
            () => new DecimalAggregator().Aggregate(context, secondExpression, decimalAggregatorDelegate),
            () => Result<object?>.Invalid("Context is not of a supported type")
        };

        return delegates.Select(x => x.Invoke()).First(x => x.Status != ResultStatus.NotSupported);
    }
}

