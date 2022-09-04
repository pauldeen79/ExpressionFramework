namespace ExpressionFramework.Domain;

public class ExpressionEvaluator : IExpressionEvaluator
{
    private readonly IEnumerable<IExpressionHandler> _handlers;

    public ExpressionEvaluator(IEnumerable<IExpressionHandler> handlers)
        => _handlers = handlers;

    public async Task<Result<object?>> Evaluate(object? context, Expression expression)
    {
        foreach (var handler in _handlers)
        {
            var handlerResult = await handler.Handle(context, expression, this);
            if (handlerResult.Status == ResultStatus.NotSupported)
            {
                continue;
            }

            return handlerResult;
        }

        return Result<object?>.Invalid($"Unknown expression: {expression}");
    }
}
