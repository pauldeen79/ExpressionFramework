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

        return Evaluate(context, typedExpression, evaluator);
    }

    protected abstract Task<Result<object?>> Evaluate(object? context, T typedExpression, IExpressionEvaluator evaluator);
}
