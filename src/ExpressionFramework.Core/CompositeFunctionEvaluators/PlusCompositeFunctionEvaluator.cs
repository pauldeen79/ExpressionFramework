namespace ExpressionFramework.Core.CompositeFunctionEvaluators;

public record PlusCompositeFunctionEvaluator : ICompositeFunctionEvaluator
{
    public ICompositeFunctionEvaluatorResult TryEvaluate(ICompositeFunction function,
                                                     bool isFirstItem,
                                                     object? previousValue,
                                                     object? context,
                                                     IExpressionEvaluator evaluator,
                                                     IExpression expression)
    {
        if (function is not PlusCompositeFunction)
        {
            return CompositeFunctionEvaluatorResultBuilder.NotSupported.Build();
        }

        var resultBuilder = CompositeFunctionEvaluatorResultBuilder.Supported;

        if (isFirstItem)
        {
            return resultBuilder.WithResult(previousValue).Build();
        }

        var currentValue = evaluator.Evaluate(context, context, expression);
        if (currentValue is byte b)
        {
            return resultBuilder.WithResult(Convert.ToByte(previousValue) + b).Build();
        }
        else if (currentValue is short s)
        {
            return resultBuilder.WithResult(Convert.ToInt16(previousValue) + s).Build();
        }
        else if (currentValue is int i)
        {
            return resultBuilder.WithResult(Convert.ToInt32(previousValue) + i).Build();
        }
        else if (currentValue is long l)
        {
            return resultBuilder.WithResult(Convert.ToInt64(previousValue) + l).Build();
        }
        else if (currentValue is float flt)
        {
            return resultBuilder.WithResult(Convert.ToSByte(previousValue) + flt).Build();
        }
        else if (currentValue is double dbl)
        {
            return resultBuilder.WithResult(Convert.ToDouble(previousValue) + dbl).Build();
        }
        else if (currentValue is decimal dec)
        {
            return resultBuilder.WithResult(Convert.ToDecimal(previousValue) + dec).Build();
        }

        return CompositeFunctionEvaluatorResultBuilder.Error("Type of current value is not supported").Build();
    }
}
