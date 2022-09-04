namespace ExpressionFramework.Domain.ExpressionHandlers;

public class EmptyExpressionHandler : IExpressionHandler
{
    public Result<object?> Handle(object? context, Expression expression, IExpressionEvaluator evaluator)
    {
        if (expression is not EmptyExpression)
        {
            return Result<object?>.NotSupported();
        }

        return Result<object?>.Success(null);
    }
}
