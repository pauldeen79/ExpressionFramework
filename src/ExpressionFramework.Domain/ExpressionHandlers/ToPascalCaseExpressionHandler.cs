namespace ExpressionFramework.Domain.ExpressionHandlers;

public class ToPascalCaseExpressionHandler : ExpressionHandlerBase<ToPascalCaseExpression>
{
    protected override Task<Result<object?>> Evaluate(object? context, ToPascalCaseExpression typedExpression, IExpressionEvaluator evaluator)
        => Task.FromResult(Result<object?>.Success(ToPascalCase(context?.ToString())));

    private string? ToPascalCase(string? value)
    {
        if (value != null && !string.IsNullOrEmpty(value))
        {
            return value.Substring(0, 1).ToLowerInvariant() + value.Substring(1);
        }

        return value;
    }
}
