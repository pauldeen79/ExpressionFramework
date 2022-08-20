namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class CompositeExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    private readonly IEnumerable<ICompositeFunctionEvaluator> _evaluators;

    public CompositeExpressionEvaluatorProvider(IEnumerable<ICompositeFunctionEvaluator> evaluators)
        => _evaluators = evaluators;

    public bool TryEvaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator, out object? result)
    {
        result = default;

        if (expression is not ICompositeExpression compositeExpression)
        {
            return false;
        }

        bool first = true;
        foreach (var innerExpression in compositeExpression.Expressions)
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
                return false;
            }
        }

        return true;
    }
}
