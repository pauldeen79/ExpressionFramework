namespace ExpressionFramework.Core.AggregateFunctionEvaluators;

[ExcludeFromCodeCoverage]
internal class EmptyAggegateFunctionEvaluator : IAggregateFunctionEvaluator
{
    public Result<IAggregateFunctionResultValue?> TryEvaluate(IAggregateFunction function,
                                                         bool isFirstItem,
                                                         object? value,
                                                         object? context,
                                                         IExpressionEvaluator evaluator,
                                                         IExpression expression)
    {
        if (function is not EmptyAggregateFunction)
        {
            return Result<IAggregateFunctionResultValue?>.NotSupported();
        }

        return Result<IAggregateFunctionResultValue?>.Error("No aggregate function was selected");
    }
}
