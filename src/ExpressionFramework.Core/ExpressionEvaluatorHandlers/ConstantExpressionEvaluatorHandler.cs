namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class ConstantExpressionEvaluatorHandler : IExpressionEvaluatorHandler
{
    public Result<object?> Handle(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IConstantExpression constantExpression)
        {
            return Result<object?>.Success(constantExpression.Value);
        }

        return Result<object?>.NotSupported();
    }
}
