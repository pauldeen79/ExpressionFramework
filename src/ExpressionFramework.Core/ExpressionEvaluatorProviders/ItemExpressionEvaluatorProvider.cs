namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class ItemExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    public bool TryEvaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator, out object? result)
    {
        if (expression is IItemExpression)
        {
            result = item;
            return true;
        }

        result = default;
        return false;
    }
}
