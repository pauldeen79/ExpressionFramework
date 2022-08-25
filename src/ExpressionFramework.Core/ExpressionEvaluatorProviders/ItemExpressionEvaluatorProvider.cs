namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class ItemExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    public Result<object?> Evaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IItemExpression)
        {
            return Result<object?>.Success(item);
        }

        return Result<object?>.NotSupported();
    }
}
