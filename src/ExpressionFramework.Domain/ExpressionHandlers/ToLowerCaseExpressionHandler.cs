namespace ExpressionFramework.Domain.ExpressionHandlers;

public class ToLowerCaseExpressionHandler : ExpressionHandlerBase<ToLowerCaseExpression>
{
    protected override Task<Result<object?>> Handle(object? context, ToLowerCaseExpression typedExpression, IExpressionEvaluator evaluator)
        => Task.FromResult(Result<object?>.Success(context?.ToString().ToLower()));
}
