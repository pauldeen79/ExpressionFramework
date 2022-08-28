namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class EmptyExpressionEvaluatorHandler : IExpressionEvaluatorHandler
{
    public Result<object?> Handle(object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IEmptyExpression constantExpression)
        {
            return Result<object?>.Success(default);
        }

        return Result<object?>.NotSupported();
    }
}
