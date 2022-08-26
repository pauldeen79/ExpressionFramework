namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class ItemExpressionEvaluatorHandler : IExpressionEvaluatorHandler
{
    public Result<object?> Handle(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IItemExpression)
        {
            return Result<object?>.Success(item);
        }

        return Result<object?>.NotSupported();
    }
}
