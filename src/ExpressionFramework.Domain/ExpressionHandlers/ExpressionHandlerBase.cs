namespace ExpressionFramework.Domain.ExpressionHandlers;

public abstract class ExpressionHandlerBase<T> : IExpressionHandler
    where T : Expression
{
    public Task<Result<object?>> Handle(object? context, Expression expression, IExpressionEvaluator evaluator)
    {
        if (expression is not T typedExpression)
        {
            return Task.FromResult(Result<object?>.NotSupported());
        }

        return Handle(context, typedExpression, evaluator);
    }

    protected abstract Task<Result<object?>> Handle(object? context, T typedExpression, IExpressionEvaluator evaluator);
}
