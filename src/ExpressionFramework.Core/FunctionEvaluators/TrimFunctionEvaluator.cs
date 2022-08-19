namespace ExpressionFramework.Core.FunctionEvaluators;

public class TrimFunctionEvaluator : IFunctionEvaluator
{
    public bool TryEvaluate(IExpressionFunction function, object? value, object? sourceItem, IExpressionEvaluator evaluator, out object? result)
    {
        if (!(function is TrimFunction))
        {
            result = null;
            return false;
        }

        result = value == null
            ? string.Empty
            : value.ToString().Trim();
        return true;
    }
}
