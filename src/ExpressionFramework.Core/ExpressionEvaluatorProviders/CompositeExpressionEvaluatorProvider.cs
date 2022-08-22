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

    public bool TryEvaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator, out object? result)
    {
        result = default;

        if (expression is not ICompositeExpression compositeExpression)
        {
            return false;
        }

        bool first = true;
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
                ProcessSubSequentExpression(item, evaluator, ref result, compositeExpression, innerExpression, ref shouldContinue);

                if (!shouldContinue)
                {
                    break;
                }
            }
        }

        return true;
    }

    private object? ProcessFirstExpression(object? item, object? context, IExpressionEvaluator evaluator, ICompositeExpression compositeExpression, IExpression innerExpression, ref bool shouldContinue)
    {
        var result = evaluator.Evaluate(item, context, innerExpression);

        var found = false;
        foreach (var eval in _evaluators)
        {
            var evalResult = eval.TryEvaluate(compositeExpression.CompositeFunction, true, result, item, evaluator, innerExpression);
            if (evalResult.IsSupported)
            {
                found = true;
                result = evalResult.Result;
                shouldContinue = evalResult.ShouldContinue;
                if (!string.IsNullOrEmpty(evalResult.ErrorMessage))
                {
                    // Something went wrong in the composite function evaluator
                    result = evalResult.ErrorMessage;
                }
                break;
            }
        }

        if (!found)
        {
            // Unknown composite function evaluator
            result = default;
        }

        return result;
    }

    private void ProcessSubSequentExpression(object? item, IExpressionEvaluator evaluator, ref object? result, ICompositeExpression compositeExpression, IExpression innerExpression, ref bool shouldContinue)
    {
        var found = false;
        foreach (var eval in _evaluators)
        {
            var evalResult = eval.TryEvaluate(compositeExpression.CompositeFunction, false, result, item, evaluator, innerExpression);
            if (evalResult.IsSupported)
            {
                found = true;
                result = evalResult.Result;
                shouldContinue = evalResult.ShouldContinue;
                if (!string.IsNullOrEmpty(evalResult.ErrorMessage))
                {
                    // Something went wrong in the composite function evaluator
                    result = evalResult.ErrorMessage;
                }
                break;
            }
        }

        if (!found)
        {
            // Unknown composite function evaluator
            result = default;
        }
    }

    private IEnumerable<IExpression> GetValidExpressions(ICompositeExpression compositeExpression,
                                                         object? item,
                                                         object? context,
                                                         IExpressionEvaluator expressionEvaluator,
                                                         IConditionEvaluator conditionEvaluator)
        => compositeExpression.Expressions
            .Where(expression => conditionEvaluator.Evaluate(expressionEvaluator.Evaluate(item, context, expression),
                                                             compositeExpression.ExpressionConditions));
}
