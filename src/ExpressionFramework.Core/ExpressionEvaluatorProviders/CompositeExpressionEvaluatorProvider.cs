using ExpressionFramework.Core.Default;

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
                result = evaluator.Evaluate(item, context, innerExpression);
                System.Diagnostics.Debug.WriteLine($"First pure result: {result}");

                var found = false;
                foreach (var eval in _evaluators)
                {
                    if (eval.TryEvaluate(compositeExpression.CompositeFunction, true, result, item, evaluator, innerExpression, out var evaluatorResult, out shouldContinue))
                    {
                        found = true;
                        result = evaluatorResult;
                        System.Diagnostics.Debug.WriteLine($"Composite result of first pure result: {result}");
                        break;
                    }
                }

                if (!found)
                {
                    // Unknown composite function evaluator
                    result = default;
                }

                if (!shouldContinue)
                {
                    break;
                }

                first = false;
            }
            else
            {
                var found = false;
                foreach (var eval in _evaluators)
                {
                    if (eval.TryEvaluate(compositeExpression.CompositeFunction, false, result, item, evaluator, innerExpression, out var evaluatorResult, out shouldContinue))
                    {
                        found = true;
                        result = evaluatorResult;
                        System.Diagnostics.Debug.WriteLine($"Subsequent result: {result}");
                        break;
                    }
                }

                if (!found)
                {
                    // Unknown composite function evaluator
                    result = default;
                }

                if (!shouldContinue)
                {
                    break;
                }
            }
        }

        return true;
    }

    private IEnumerable<IExpression> GetValidExpressions(ICompositeExpression compositeExpression,
                                                         object? item,
                                                         object? context,
                                                         IExpressionEvaluator expressionEvaluator,
                                                         IConditionEvaluator conditionEvaluator)
    {
        return compositeExpression.Expressions
            .Where(expression =>
            {
                var x = conditionEvaluator.Evaluate(expressionEvaluator.Evaluate(item, context, expression), compositeExpression.ExpressionConditions);
                System.Diagnostics.Debug.WriteLine($"Expression [{expression}] evaluates to {x}");
                return x;
            });
    }
}
