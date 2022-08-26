namespace ExpressionFramework.Core.AggregateFunctionEvaluators;

public record PlusAggregateFunctionEvaluator : IAggregateFunctionEvaluator
{
    public Result<IAggregateFunctionResultValue?> TryEvaluate(IAggregateFunction function,
                                       bool isFirstItem,
                                       object? value,
                                       object? context,
                                       IExpressionEvaluator evaluator,
                                       IExpression expression)
    {
        if (function is not PlusAggregateFunction)
        {
            return Result<IAggregateFunctionResultValue?>.NotSupported();
        }

        if (isFirstItem)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(value).Build());
        }

        var result = evaluator.Evaluate(context, context, expression);
        if (!result.IsSuccessful())
        {
            return Result<IAggregateFunctionResultValue?>.Error(result.ErrorMessage.WhenNullOrEmpty("Something went wrong"));
        }

        var currentValue = result.GetValueOrThrow();
        if (currentValue is byte b)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToByte(value) + b).Build());
        }
        else if (currentValue is short s)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToInt16(value) + s).Build());
        }
        else if (currentValue is int i)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToInt32(value) + i).Build());
        }
        else if (currentValue is long l)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToInt64(value) + l).Build());
        }
        else if (currentValue is float flt)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToSByte(value) + flt).Build());
        }
        else if (currentValue is double dbl)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToDouble(value) + dbl).Build());
        }
        else if (currentValue is decimal dec)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToDecimal(value) + dec).Build());
        }

        return Result<IAggregateFunctionResultValue?>.Error("Type of current value is not supported");
    }
}
