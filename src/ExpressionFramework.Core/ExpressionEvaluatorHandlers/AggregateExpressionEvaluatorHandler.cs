namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class AggregateExpressionEvaluatorHandler : IExpressionEvaluatorHandler
{
    private readonly IConditionEvaluatorProvider _conditionEvaluatorProvider;
    private readonly IEnumerable<IAggregateFunctionEvaluator> _evaluators;

    public AggregateExpressionEvaluatorHandler(
        IConditionEvaluatorProvider conditionEvaluatorProvider,
        IEnumerable<IAggregateFunctionEvaluator> evaluators)
    {
        _conditionEvaluatorProvider = conditionEvaluatorProvider;
        _evaluators = evaluators;
    }

    public Result<object?> Handle(object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is not IAggregateExpression aggregateExpression)
        {
            return Result<object?>.NotSupported();
        }

        var validExpressionsResult = GetValidExpressions(aggregateExpression,
                                                         context,
                                                         evaluator,
                                                         _conditionEvaluatorProvider.Get(evaluator));
        if (!validExpressionsResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(validExpressionsResult);
        }

        bool first = true;
        Result<object?>? result = null;
        foreach (var exp in validExpressionsResult.Value!)
        {
            var shouldContinue = true;
            if (first)
            {
                result = ProcessFirstExpression(context,
                                                evaluator,
                                                aggregateExpression, 
                                                exp,
                                                ref shouldContinue);
                if (!shouldContinue)
                {
                    break;
                }

                first = false;
            }
            else
            {
                result = ProcessSubSequentExpression(context,
                                                     evaluator,
                                                     result!,
                                                     aggregateExpression,
                                                     exp,
                                                     ref shouldContinue);
                if (!shouldContinue)
                {
                    break;
                }
            }
        }

        return result ?? Result<object?>.Invalid("No expressions found");
    }

    private Result<object?> ProcessFirstExpression(object? context,
                                                   IExpressionEvaluator evaluator,
                                                   IAggregateExpression aggregateExpression,
                                                   IExpression innerExpression,
                                                   ref bool shouldContinue)
        => ProcessExpression(context,
                             evaluator,
                             aggregateExpression,
                             innerExpression,
                             out shouldContinue,
                             evaluator.Evaluate(context, innerExpression),
                             true);

    private Result<object?> ProcessSubSequentExpression(object? context,
                                                        IExpressionEvaluator evaluator,
                                                        Result<object?> previousResult,
                                                        IAggregateExpression aggregateExpression,
                                                        IExpression innerExpression,
                                                        ref bool shouldContinue)
        => ProcessExpression(context,
                             evaluator,
                             aggregateExpression,
                             innerExpression,
                             out shouldContinue,
                             previousResult,
                             false);

    private Result<object?> ProcessExpression(object? context,
                                              IExpressionEvaluator evaluator,
                                              IAggregateExpression aggregateExpression,
                                              IExpression innerExpression,
                                              out bool shouldContinue,
                                              Result<object?> result,
                                              bool isFirstExpression)
    {
        foreach (var eval in _evaluators)
        {
            var evalResult = eval.Evaluate(aggregateExpression.AggregateFunction,
                                           isFirstExpression,
                                           result.Value,
                                           context,
                                           evaluator,
                                           innerExpression);
            if (evalResult.IsSupported())
            {
                shouldContinue = evalResult.ShouldContinue();
                return Result<object?>.FromExistingResult(evalResult, evalResult.GetResultValue());
            }
        }

        shouldContinue = false;
        return Result<object?>.Invalid($"Unknown aggregate function: [{aggregateExpression.AggregateFunction}]");
    }

    private Result<IEnumerable<IExpression>> GetValidExpressions(IAggregateExpression aggregateExpression,
                                                                 object? context,
                                                                 IExpressionEvaluator expressionEvaluator,
                                                                 IConditionEvaluator conditionEvaluator)
    {
        var valid = new List<IExpression>();
        Result<IEnumerable<IExpression>>? invalidResult = null;
        foreach (var @expression in aggregateExpression.Expressions)
        {
            var expressionResult = expressionEvaluator.Evaluate(context, expression);
            if (!expressionResult.IsSuccessful())
            {
                invalidResult = Result<IEnumerable<IExpression>>.FromExistingResult(expressionResult);
                break;
            }
            var conditionResult = conditionEvaluator.Evaluate(expressionResult.Value, aggregateExpression.ExpressionConditions);
            if (!conditionResult.IsSuccessful())
            {
                invalidResult = Result<IEnumerable<IExpression>>.FromExistingResult(conditionResult);
                break;
            }
            if (conditionResult.Value)
            {
                valid.Add(@expression);
            }
        }

        if (invalidResult != null)
        {
            return invalidResult;
        }

        return Result<IEnumerable<IExpression>>.Success(valid);
    }
}
