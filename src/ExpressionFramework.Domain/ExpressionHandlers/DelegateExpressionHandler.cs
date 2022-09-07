namespace ExpressionFramework.Domain.ExpressionHandlers;

public class DelegateExpressionHandler : ExpressionHandlerBase<DelegateExpression>
{
    protected override Task<Result<object?>> Evaluate(object? context, DelegateExpression typedExpression, IExpressionEvaluator evaluator)
        => Task.FromResult(Result<object?>.Success(typedExpression.Value.Invoke(new DelegateExpressionRequest(context, evaluator))));
}
