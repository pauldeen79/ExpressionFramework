namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class DelegateExpressionEvaluatorHandler : IExpressionEvaluatorHandler
{
    public Result<object?> Handle(object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is IDelegateExpression delegateExpression)
        {
            return Result<object?>.Success(delegateExpression.ValueDelegate.Invoke(new DelegateExpressionRequestBuilder().WithContext(context).WithExpression(expression.ToBuilder()).WithEvaluator(evaluator).Build()).Result);
        }

        return Result<object?>.NotSupported();
    }
}
