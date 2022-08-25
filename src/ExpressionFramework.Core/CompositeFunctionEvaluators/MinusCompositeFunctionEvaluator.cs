namespace ExpressionFramework.Core.CompositeFunctionEvaluators;

public class MinusCompositeFunctionEvaluator : ICompositeFunctionEvaluator
{
    public ICompositeFunctionEvaluatorResult TryEvaluate(ICompositeFunction function,
                                                         bool isFirstItem,
                                                         object? value,
                                                         object? context,
                                                         IExpressionEvaluator evaluator,
                                                         IExpression expression)
    {
        if (function is not MinusCompositeFunction)
        {
            return CompositeFunctionEvaluatorResultBuilder.NotSupported.Build();
        }

        var resultBuilder = CompositeFunctionEvaluatorResultBuilder.Supported;

        if (isFirstItem)
        {
            return resultBuilder.WithResult(value).Build();
        }

        var result = evaluator.Evaluate(context, context, expression);
        if (!result.IsSuccessful())
        {
            return CompositeFunctionEvaluatorResultBuilder.Error(result.ErrorMessage.WhenNullOrEmpty("Something went wrong")).Build();
        }

        var currentValue = result.GetValueOrThrow();
        if (currentValue is byte b)
        {
            return resultBuilder.WithResult(Convert.ToByte(value) - b).Build();
        }
        else if (currentValue is short s)
        {
            return resultBuilder.WithResult(Convert.ToInt16(value) - s).Build();
        }
        else if (currentValue is int i)
        {
            return resultBuilder.WithResult(Convert.ToInt32(value) - i).Build();
        }
        else if (currentValue is long l)
        {
            return resultBuilder.WithResult(Convert.ToInt64(value) - l).Build();
        }
        else if (currentValue is float flt)
        {
            return resultBuilder.WithResult(Convert.ToSByte(value) - flt).Build();
        }
        else if (currentValue is double dbl)
        {
            return resultBuilder.WithResult(Convert.ToDouble(value) - dbl).Build();
        }
        else if (currentValue is decimal dec)
        {
            return resultBuilder.WithResult(Convert.ToDecimal(value) - dec).Build();
        }

        return CompositeFunctionEvaluatorResultBuilder.Error("Type of current value is not supported").Build();
    }
}
