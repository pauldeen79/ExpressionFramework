namespace ExpressionFramework.Domain.Aggregators;

public static class AggregatorBase
{
    public static Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression, ITypedExpression<IFormatProvider>? formatProviderExpression, Func<object?, object?, IFormatProvider, Result<object?>> aggregateDelegate)
    {
        if (firstExpression is null)
        {
            return Result<object?>.Invalid("First expression is required");
        }

        if (secondExpression is null)
        {
            return Result<object?>.Invalid("Second expression is required");
        }

        if (aggregateDelegate is null)
        {
            return Result<object?>.Invalid("Aggregate expression is required");
        }

        var firstExpressionResult = firstExpression.Evaluate(context);
        if (!firstExpressionResult.IsSuccessful())
        {
            return firstExpressionResult;
        }

        var secondExpressionResult = secondExpression.Evaluate(context);
        if (!secondExpressionResult.IsSuccessful())
        {
            return secondExpressionResult;
        }

        var formatProviderExpressionResult = formatProviderExpression == null
            ? Result<IFormatProvider>.Success(CultureInfo.InvariantCulture)
            : formatProviderExpression.EvaluateTyped(context);

        if (!formatProviderExpressionResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(formatProviderExpressionResult);
        }

        return aggregateDelegate.Invoke(firstExpressionResult.Value, secondExpressionResult.Value, formatProviderExpressionResult.Value!);
    }
}
