namespace ExpressionFramework.Core.AggregateFunctionEvaluators;

public record FirstAggregateFunctionEvaluator : IAggregateFunctionEvaluator
{
    public Result<IAggregateFunctionResultValue?> TryEvaluate(IAggregateFunction function,
                                                              bool isFirstItem,
                                                              object? value,
                                                              object? context,
                                                              IExpressionEvaluator evaluator,
                                                              IExpression expression)
    {
        if (function is not FirstAggregateFunction)
        {
            return Result<IAggregateFunctionResultValue?>.NotSupported();
        }

        if (isFirstItem)
        {
            return Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(value).Stop().Build());
        }

        return Result<IAggregateFunctionResultValue?>.Error("Not supposed to come here, as we said to stop!");
    }
}
