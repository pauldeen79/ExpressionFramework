namespace ExpressionFramework.Core.AggregateFunctionEvaluators;

public abstract class AggregateFunctionEvaluatorBase<TFunction> : IAggregateFunctionEvaluator
    where TFunction : IAggregateFunction
{
    public Result<IAggregateFunctionResultValue?> TryEvaluate(IAggregateFunction function,
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
            return Result<IAggregateFunctionResultValue?>.Error(result.ErrorMessage.WhenNullOrEmpty("Something went wrong"));
        }

        return TryEvaluate(tfunction, isFirstItem, value, context, evaluator, expression, result);
    }

    protected abstract Result<IAggregateFunctionResultValue?> TryEvaluate(TFunction function,
                                                                          bool isFirstItem,
                                                                          object? value,
                                                                          object? context,
                                                                          IExpressionEvaluator evaluator,
                                                                          IExpression expression,
                                                                          Result<object?> result);
}
