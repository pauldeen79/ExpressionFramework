namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class EmptyExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    public Result<object?> Evaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IEmptyExpression constantExpression)
        {
            return Result<object?>.Success(default);
        }

        return Result<object?>.NotSupported();
    }
}
