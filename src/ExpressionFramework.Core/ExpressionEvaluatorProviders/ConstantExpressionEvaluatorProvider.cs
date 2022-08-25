namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class ConstantExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    public Result<object?> Evaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IConstantExpression constantExpression)
        {
            return Result<object?>.Success(constantExpression.Value);
        }

        return Result<object?>.NotSupported();
    }
}
