﻿namespace ExpressionFramework.Domain.Aggregators;

[AggregatorDescription("First value to the power of second value")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Power of two values", "This will be returned in case the execution returns success (Ok)")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Could not convert FirstExpression to [Type]. Error message: [Error message], Could not convert SecondExpression to [Type]. Error message: [Error message]")]
public partial record PowerAggregator
{
    public override Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression)
        => NumericAggregator.Evaluate(context,
                                      firstExpression,
                                      secondExpression,
                                      new Func<byte, byte, object>((b1, b2) => b1 ^ b2),
                                      new Func<short, short, object>((s1, s2) => s1 ^ s2),
                                      new Func<int, int, object>((i1, i2) => i1 ^ i2),
                                      new Func<long, long, object>((l1, l2) => l1 ^ l2),
                                      new Func<float, float, object>((f1, f2) => Math.Pow(f1, f2)),
                                      new Func<decimal, decimal, object>((d1, d2) => Math.Pow(Convert.ToDouble(d1), Convert.ToDouble(d2))),
                                      new Func<double, double, object>((d1, d2) => Math.Pow(d1, d2)));
}
