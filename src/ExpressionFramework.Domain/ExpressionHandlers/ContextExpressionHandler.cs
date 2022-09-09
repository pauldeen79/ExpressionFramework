namespace ExpressionFramework.Domain.ExpressionHandlers;

public class ContextExpressionHandler : ExpressionHandlerBase<ContextExpression>
{
    protected override Task<Result<object?>> Handle(object? context, ContextExpression typedExpression, IExpressionEvaluator evaluator)
        => Task.FromResult(Result<object?>.Success(context));
}
