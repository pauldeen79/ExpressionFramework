namespace ExpressionFramework.Core;

public class AddAggregatorFunction : IFunction, IAggregator
{
    public Result<object?> Aggregate(object value1, object value2, IFormatProvider formatProvider)
        => Add.Evaluate(value1, value2, formatProvider);

    public Result<object?> Evaluate(FunctionCallContext context)
        => Result.Success<object?>(this);
}
