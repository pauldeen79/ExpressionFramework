namespace ExpressionFramework.Domain.Aggregators;

public partial record PowerAggregator
{
    public override Result<object?> Aggregate(object? context, Expression secondExpression)
        => NumericAggregator.Evaluate(context,
                                      secondExpression,
                                      new Func<byte, byte, object>((b1, b2) => b1 ^ b2),
                                      new Func<short, short, object>((s1, s2) => s1 ^ s2),
                                      new Func<int, int, object>((i1, i2) => i1 ^ i2),
                                      new Func<long, long, object>((l1, l2) => l1 ^ l2),
                                      new Func<float, float, object>((f1, f2) => Math.Pow(f1, f2)),
                                      new Func<decimal, decimal, object>((d1, d2) => Math.Pow(Convert.ToDouble(d1), Convert.ToDouble(d2))),
                                      new Func<double, double, object>((d1, d2) => Math.Pow(d1, d2)));
}

