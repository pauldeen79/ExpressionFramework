namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class AggregateExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    private readonly IConditionEvaluatorProvider _conditionEvaluatorProvider;
    private readonly IEnumerable<IAggregateFunctionEvaluator> _evaluators;

    public AggregateExpressionEvaluatorProvider(
        IConditionEvaluatorProvider conditionEvaluatorProvider,
        IEnumerable<IAggregateFunctionEvaluator> evaluators)
    {
        _conditionEvaluatorProvider = conditionEvaluatorProvider;
        _evaluators = evaluators;
    }

    public Result<object?> Evaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is not IAggregateExpression aggregateExpression)
        {
            return Result<object?>.NotSupported();
        }

        bool first = true;
        Result<object?>? result = null;
        var expressions = GetValidExpressions(aggregateExpression, item, context, evaluator, _conditionEvaluatorProvider.Get(evaluator));
        foreach (var innerExpression in expressions)
        {
            var shouldContinue = true;
            if (first)
            {
                result = ProcessFirstExpression(item, context, evaluator, aggregateExpression, innerExpression, ref shouldContinue);
                if (!shouldContinue)
                {
                    break;
                }

                first = false;
            }
            else
            {
                result = ProcessSubSequentExpression(item, evaluator, result!, aggregateExpression, innerExpression, ref shouldContinue);
                if (!shouldContinue)
                {
                    break;
                }
            }
        }

        return result ?? Result<object?>.Invalid("No expressions found");
    }

    private Result<object?> ProcessFirstExpression(object? item, object? context, IExpressionEvaluator evaluator, IAggregateExpression aggregateExpression, IExpression innerExpression, ref bool shouldContinue)
    {
        var firstResult = evaluator.Evaluate(item, context, innerExpression);
        if (!firstResult.IsSuccessful())
        {
            shouldContinue = false;
            return firstResult;
        }
        var result = firstResult.Value;

        foreach (var eval in _evaluators)
        {
            var evalResult = eval.Evaluate(aggregateExpression.AggregateFunction, true, result, item, evaluator, innerExpression);
            if (evalResult.IsSupported())
            {
                shouldContinue = evalResult.ShouldContinue();
                return Result<object?>.FromExistingResult(evalResult, evalResult.GetResultValue());
            }
        }

        shouldContinue = false;
        return Result<object?>.Invalid($"Unknown aggregate function: [{aggregateExpression.AggregateFunction}]");
    }

    private Result<object?> ProcessSubSequentExpression(object? item, IExpressionEvaluator evaluator, Result<object?> previousResult, IAggregateExpression aggregateExpression, IExpression innerExpression, ref bool shouldContinue)
    {
        foreach (var eval in _evaluators)
        {
            var evalResult = eval.Evaluate(aggregateExpression.AggregateFunction, false, previousResult.Value, item, evaluator, innerExpression);
            if (evalResult.IsSupported())
            {
                shouldContinue = evalResult.ShouldContinue();
                return Result<object?>.FromExistingResult(evalResult, evalResult.GetResultValue());
            }
        }

        shouldContinue = false;
        return Result<object?>.Invalid($"Unknown aggregate function: [{aggregateExpression.AggregateFunction}]");
    }

    private IEnumerable<IExpression> GetValidExpressions(IAggregateExpression aggregateExpression,
                                                         object? item,
                                                         object? context,
                                                         IExpressionEvaluator expressionEvaluator,
                                                         IConditionEvaluator conditionEvaluator)
        => aggregateExpression.Expressions
            .Where(expression => conditionEvaluator.Evaluate(expressionEvaluator.Evaluate(item, context, expression).Value,
                                                             aggregateExpression.ExpressionConditions));
}
