namespace ExpressionFramework.Core;

public class EmptyExpressionEvaluator : IExpressionEvaluator
{
    public Result<object?> Evaluate(object? context, IExpression expression)
    {
        throw new NotImplementedException("ExpressionEvaluator was not set");
    }
}
