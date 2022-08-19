namespace ExpressionFramework.Core.FunctionEvaluators;

public class ContainsFunctionEvaluator : IFunctionEvaluator
{
    public bool TryEvaluate(IExpressionFunction function, object? value, object? sourceItem, IExpressionEvaluator evaluator, out object? result)
    {
        result = null;
        if (!(function is ContainsFunction containsFunction))
        {
            return false;
        }

        var enumerable = value as IEnumerable;
        if (enumerable != null)
        {
            result = enumerable.OfType<object>().Contains(containsFunction.ObjectToContain);
        }

        return true;
    }
}
