namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class ContextExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    public bool TryEvaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator, out object? result)
    {
        if (expression is IContextExpression)
        {
            result = context;
            return true;
        }

        result = default;
        return false;
    }
}
