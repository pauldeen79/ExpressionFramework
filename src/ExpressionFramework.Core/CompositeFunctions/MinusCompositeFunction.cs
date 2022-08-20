namespace ExpressionFramework.Core.Functions;

public record MinusCompositeFunction : ICompositeFunction
{
    public string Name => nameof(MinusCompositeFunction);

    public object? Combine(object? previousValue, object? context, IExpressionEvaluator evaluator, IExpression expression)
    {
        var currentValue = evaluator.Evaluate(context, context, expression);
        if (currentValue is byte b)
        {
            return Convert.ToByte(previousValue) - b;
        }
        else if (currentValue is short s)
        {
            return Convert.ToInt16(previousValue) - s;
        }
        else if (currentValue is int i)
        {
            return Convert.ToInt32(previousValue) - i;
        }
        else if (currentValue is long l)
        {
            return Convert.ToInt64(previousValue) - l;
        }
        else if (currentValue is float flt)
        {
            return Convert.ToSingle(previousValue) - flt;
        }
        else if (currentValue is double dbl)
        {
            return Convert.ToDouble(previousValue) - dbl;
        }
        else if (currentValue is decimal dec)
        {
            return Convert.ToDecimal(previousValue) - dec;
        }

        var previousType = previousValue?.GetType()?.FullName ?? "NULL";
        var currentType = currentValue?.GetType()?.FullName ?? "NULL";
        return Result.Error($"Items are of wrong type. Only byte, short, int, long, float, double and decimal are supported. Found types: [{previousType}] and [{currentType}]");
    }

    public ICompositeFunctionBuilder ToBuilder()
        => new MinusCompositeFunctionBuilder();
}
