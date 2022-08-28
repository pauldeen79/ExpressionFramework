namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class ContextExpressionEvaluatorHandler : IExpressionEvaluatorHandler
{
    public Result<object?> Handle(object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IContextExpression)
        {
            return Result<object?>.Success(context);
        }

        return Result<object?>.NotSupported();
    }
}
