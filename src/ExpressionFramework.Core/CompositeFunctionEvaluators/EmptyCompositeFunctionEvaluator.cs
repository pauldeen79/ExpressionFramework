namespace ExpressionFramework.Core.CompositeFunctionEvaluators;

public class EmptyCompositeFunctionEvaluator : ICompositeFunctionEvaluator
{
    public bool TryEvaluate(ICompositeFunction function, object? previousValue, object? context, IExpressionEvaluator evaluator, IExpression expression, out object? result)
    {
        if (function is not EmptyCompositeFunction)
        {
            result = null;
            return false;
        }

        result = null;
        return true;
    }
}
