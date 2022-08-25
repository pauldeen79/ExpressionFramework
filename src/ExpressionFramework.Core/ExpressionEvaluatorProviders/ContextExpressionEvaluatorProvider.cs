namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class ContextExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    public Result<object?> Evaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IContextExpression)
        {
            return Result<object?>.Success(context);
        }

        return Result<object?>.NotSupported();
    }
}
