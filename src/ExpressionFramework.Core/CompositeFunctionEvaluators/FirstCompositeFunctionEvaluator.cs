namespace ExpressionFramework.Core.CompositeFunctionEvaluators;

public record FirstCompositeFunctionEvaluator : ICompositeFunctionEvaluator
{
    public bool TryEvaluate(ICompositeFunction function, object? previousValue, object? context, IExpressionEvaluator evaluator, IExpression expression, out object? result)
    {
        if (function is not FirstCompositeFunction)
        {
            result = null;
            return false;
        }

        result = previousValue;
        return true;
    }
}
