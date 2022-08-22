namespace ExpressionFramework.Core.CompositeFunctionEvaluators;

public record FirstCompositeFunctionEvaluator : ICompositeFunctionEvaluator
{
    public bool TryEvaluate(ICompositeFunction function,
                            bool isFirstItem,
                            object? previousValue,
                            object? context,
                            IExpressionEvaluator evaluator,
                            IExpression expression,
                            out object? result,
                            out bool shouldContinue)
    {
        shouldContinue = true;

        if (function is not FirstCompositeFunction)
        {
            result = null;
            return false;
        }

        if (isFirstItem)
        {
            result = previousValue;
            shouldContinue = false;
            return true;
        }

        result = previousValue;
        return true;
    }
}
