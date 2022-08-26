namespace ExpressionFramework.Core.AggregateFunctionEvaluators;

public abstract class AggregateFunctionEvaluatorBase<TFunction> : IAggregateFunctionEvaluator
    where TFunction : IAggregateFunction
{
    public Result<IAggregateFunctionResultValue?> Evaluate(IAggregateFunction function,
                                                           bool isFirstItem,
                                                           object? value,
                                                           object? context,
                                                           IExpressionEvaluator evaluator,
                                                           IExpression expression)
    {
        if (function is not TFunction tfunction)
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
            return Result<IAggregateFunctionResultValue?>.FromExistingResult(result);
        }

        return Evaluate(tfunction, isFirstItem, value, context, evaluator, expression, result);
    }

    protected abstract Result<IAggregateFunctionResultValue?> Evaluate(TFunction function,
                                                                       bool isFirstItem,
                                                                       object? value,
                                                                       object? context,
                                                                       IExpressionEvaluator evaluator,
                                                                       IExpression expression,
                                                                       Result<object?> result);
}
