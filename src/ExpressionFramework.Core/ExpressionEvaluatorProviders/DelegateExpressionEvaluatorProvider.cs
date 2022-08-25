namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class DelegateExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    public Result<object?> Evaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IDelegateExpression delegateExpression)
        {
            return Result<object?>.Success(delegateExpression.ValueDelegate.Invoke(item, expression, evaluator));
        }

        return Result<object?>.NotSupported();
    }
}
