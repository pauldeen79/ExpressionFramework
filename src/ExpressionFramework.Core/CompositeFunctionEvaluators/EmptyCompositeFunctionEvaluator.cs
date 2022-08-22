namespace ExpressionFramework.Core.CompositeFunctionEvaluators;

public class EmptyCompositeFunctionEvaluator : ICompositeFunctionEvaluator
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

        if (function is not EmptyCompositeFunction)
        {
            result = null;
            return false;
        }

        if (isFirstItem)
        {
            result = previousValue;
            return true;
        }

        result = null;
        return true;
    }
}
