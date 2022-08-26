namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class DelegateExpressionEvaluatorHandler : IExpressionEvaluatorHandler
{
    public Result<object?> Handle(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IDelegateExpression delegateExpression)
        {
            return Result<object?>.Success(delegateExpression.ValueDelegate.Invoke(item, context, expression, evaluator));
        }

        return Result<object?>.NotSupported();
    }
}
