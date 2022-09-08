namespace ExpressionFramework.Domain.ExpressionHandlers;

public class ToLowerCaseExpressionHandler : ExpressionHandlerBase<ToLowerCaseExpression>
{
    protected override Task<Result<object?>> Evaluate(object? context, ToLowerCaseExpression typedExpression, IExpressionEvaluator evaluator)
        => Task.FromResult(Result<object?>.Success(context?.ToString().ToLower()));
}
