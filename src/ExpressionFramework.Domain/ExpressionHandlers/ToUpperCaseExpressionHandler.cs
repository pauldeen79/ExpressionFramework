namespace ExpressionFramework.Domain.ExpressionHandlers;

public class ToUpperCaseExpressionHandler : ExpressionHandlerBase<ToUpperCaseExpression>
{
    protected override Task<Result<object?>> Evaluate(object? context, ToUpperCaseExpression typedExpression, IExpressionEvaluator evaluator)
        => Task.FromResult(Result<object?>.Success(context?.ToString().ToUpper()));
}
