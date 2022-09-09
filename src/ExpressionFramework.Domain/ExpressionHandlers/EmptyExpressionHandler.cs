namespace ExpressionFramework.Domain.ExpressionHandlers;

public class EmptyExpressionHandler : ExpressionHandlerBase<EmptyExpression>
{
    protected override Task<Result<object?>> Handle(object? context, EmptyExpression typedExpression, IExpressionEvaluator evaluator)
        => Task.FromResult(Result<object?>.Success(null));
}
