namespace ExpressionFramework.Core.AggregateFunctionEvaluators;

public class MultiplyAggregateFunctionEvaluator : AggregateFunctionEvaluatorBase<MultiplyAggregateFunction>
{
    protected override Result<IAggregateFunctionResultValue?> TryEvaluate(MultiplyAggregateFunction function,
                                                                          bool isFirstItem,
                                                                          object? value,
                                                                          object? context,
                                                                          IExpressionEvaluator evaluator,
                                                                          IExpression expression,
                                                                          Result<object?> result)
    {
        var currentValue = result.Value;
        if (currentValue is byte b)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToByte(value) * b).Build());
        }
        else if (currentValue is short s)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToInt16(value) * s).Build());
        }
        else if (currentValue is int i)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToInt32(value) * i).Build());
        }
        else if (currentValue is long l)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToInt64(value) * l).Build());
        }
        else if (currentValue is float flt)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToSingle(value) * flt).Build());
        }
        else if (currentValue is double dbl)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToDouble(value) * dbl).Build());
        }
        else if (currentValue is decimal dec)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(Convert.ToDecimal(value) * dec).Build());
        }

        return Result<IAggregateFunctionResultValue?>.Error("Type of current value is not supported");
    }
}
