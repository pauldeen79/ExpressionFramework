namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class ChainedExpressionEvaluatorHandler : IExpressionEvaluatorHandler
{
    public Result<object?> Handle(object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is not IChainedExpression composableExpression)
        {
            return Result<object?>.NotSupported();
        }

        Result<object?>? result = null;
        var first = true;
        foreach (var exp in composableExpression.Expressions)
        {
            if (first)
            {
                result = evaluator.Evaluate(context, exp);
                first = false;
            }
            else
            {
                result = evaluator.Evaluate(result!.Value, exp);
            }
            if (!result.IsSuccessful())
            {
                return result;
            }
        }

        return result ?? Result<object?>.Invalid("No expressions found");
    }
}
