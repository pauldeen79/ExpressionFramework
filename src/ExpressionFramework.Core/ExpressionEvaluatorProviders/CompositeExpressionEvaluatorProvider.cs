namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class CompositeExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    private readonly IConditionEvaluatorProvider _conditionEvaluatorProvider;
    private readonly IEnumerable<ICompositeFunctionEvaluator> _evaluators;

    public CompositeExpressionEvaluatorProvider(
        IConditionEvaluatorProvider conditionEvaluatorProvider,
        IEnumerable<ICompositeFunctionEvaluator> evaluators)
    {
        _conditionEvaluatorProvider = conditionEvaluatorProvider;
        _evaluators = evaluators;
    }

    public Result<object?> Evaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is not ICompositeExpression compositeExpression)
        {
            return Result<object?>.NotSupported();
        }

        bool first = true;
        Result<object?>? result = null;
        var expressions = GetValidExpressions(compositeExpression, item, context, evaluator, _conditionEvaluatorProvider.Get(evaluator));
        foreach (var innerExpression in expressions)
        {
            var shouldContinue = true;
            if (first)
            {
                result = ProcessFirstExpression(item, context, evaluator, compositeExpression, innerExpression, ref shouldContinue);

                if (!shouldContinue)
                {
                    break;
                }

                first = false;
            }
            else
            {
                result = ProcessSubSequentExpression(item, evaluator, result!, compositeExpression, innerExpression, ref shouldContinue);

                if (!shouldContinue)
                {
                    break;
                }
            }
        }

        return result ?? Result<object?>.Invalid("No expressions found");
    }

    private Result<object?> ProcessFirstExpression(object? item, object? context, IExpressionEvaluator evaluator, ICompositeExpression compositeExpression, IExpression innerExpression, ref bool shouldContinue)
    {
        var firstResult = evaluator.Evaluate(item, context, innerExpression);
        if (!firstResult.IsSuccessful())
        {
            return firstResult;
        }
        var result = firstResult.Value;

        foreach (var eval in _evaluators)
        {
            var evalResult = eval.TryEvaluate(compositeExpression.CompositeFunction, true, result, item, evaluator, innerExpression);
            if (evalResult.IsSupported)
            {
                shouldContinue = evalResult.ShouldContinue;
                if (!string.IsNullOrEmpty(evalResult.ErrorMessage))
                {
                    // Something went wrong in the composite function evaluator
                    return Result<object?>.Error(evalResult.ErrorMessage!);
                }
                return Result<object?>.Success(evalResult.Result);
            }
        }

        // Unknown composite function evaluator
        shouldContinue = false;
        return Result<object?>.Invalid($"Unknown composite function: [{compositeExpression.CompositeFunction}]");
    }

    private Result<object?> ProcessSubSequentExpression(object? item, IExpressionEvaluator evaluator, Result<object?> previousResult, ICompositeExpression compositeExpression, IExpression innerExpression, ref bool shouldContinue)
    {
        foreach (var eval in _evaluators)
        {
            var evalResult = eval.TryEvaluate(compositeExpression.CompositeFunction, false, previousResult.Value, item, evaluator, innerExpression);
            if (evalResult.IsSupported)
            {
                shouldContinue = evalResult.ShouldContinue;
                if (!string.IsNullOrEmpty(evalResult.ErrorMessage))
                {
                    // Something went wrong in the composite function evaluator
                    shouldContinue = false;
                    return Result<object?>.Error(evalResult.ErrorMessage!);
                }
                return Result<object?>.Success(evalResult.Result);
            }
        }

        // Unknown composite function evaluator
        shouldContinue = false;
        return Result<object?>.Invalid($"Unknown composite function: [{compositeExpression.CompositeFunction}]");
    }

    private IEnumerable<IExpression> GetValidExpressions(ICompositeExpression compositeExpression,
                                                         object? item,
                                                         object? context,
                                                         IExpressionEvaluator expressionEvaluator,
                                                         IConditionEvaluator conditionEvaluator)
        => compositeExpression.Expressions
            .Where(expression => conditionEvaluator.Evaluate(expressionEvaluator.Evaluate(item, context, expression).Value,
                                                             compositeExpression.ExpressionConditions));
}
