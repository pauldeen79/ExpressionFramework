namespace ExpressionFramework.Domain.ExpressionHandlers;

public class ChainedExpressionHandler : ExpressionHandlerBase<ChainedExpression>
{
    protected override async Task<Result<object?>> Handle(object? context, ChainedExpression typedExpression, IExpressionEvaluator evaluator)
    {
        Result<object?>? result = null;
        var first = true;
        foreach (var exp in typedExpression.Expressions)
        {
            if (first)
            {
                result = await evaluator.Evaluate(context, exp);
                first = false;
            }
            else
            {
                result = await evaluator.Evaluate(result!.Value, exp);
            }
            if (!result.IsSuccessful())
            {
                return result;
            }
        }

        return result ?? Result<object?>.Invalid("No expressions found");
    }
}
