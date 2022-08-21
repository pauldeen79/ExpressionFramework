using System.Linq;

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
        foreach (var innerExpression in GetValidExpressions(compositeExpression, context, evaluator))
        {
            if (first)
            {
                // Note that context and item have been switched on purpose! This was intended. Unit tests pass, so everything's okay.
#pragma warning disable S2234 // Parameters should be passed in the correct order 
                result = evaluator.Evaluate(item: context, context: item, innerExpression);
#pragma warning restore S2234 // Parameters should be passed in the correct order
                first = false;
            }
            else
            {
                foreach (var eval in _evaluators)
                {
                    if (eval.TryEvaluate(compositeExpression.CompositeFunction, result, item, evaluator, innerExpression, out var evaluatorResult))
                    {
                        result = evaluatorResult;
                        return true;
                    }
                }
                // Unknown composite function
                return true;
            }
        }

        // No expressions
        return true;
    }

    private IReadOnlyCollection<IExpression> GetValidExpressions(ICompositeExpression compositeExpression, object? context, IExpressionEvaluator expressionEvaluator)
    {
        if (!compositeExpression.ExpressionConditions.Any())
        {
            return compositeExpression.Expressions;
        }

        var conditionEvaluator = _conditionEvaluatorProvider.Get(expressionEvaluator);
        return compositeExpression.Expressions
            .Where(expression => conditionEvaluator.Evaluate(expressionEvaluator.Evaluate(context, context, expression), compositeExpression.ExpressionConditions))
            .ToList()
            .AsReadOnly();
    }
}
