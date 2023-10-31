namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(TrimEndExpression))]
public partial record TrimEndExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped(context).Transform(result =>
            result.IsSuccessful()
                ? TrimEnd(context, result.Value)
                : result);

    private Result<string> TrimEnd(object? context, string? s)
    {
        if (s is null)
        {
            return Result.Invalid<string>("Expression is not of type string");
        }

        if (TrimCharsExpression is null)
        {
            return Result.Success<string>(s.TrimEnd());
        }

        var trimCharsResult = TrimCharsExpression.EvaluateTyped(context);
        if (!trimCharsResult.IsSuccessful())
        {
            return Result.FromExistingResult<string>(trimCharsResult);
        }

        if (trimCharsResult.Value == null)
        {
            return Result.Success<string>(s.TrimEnd());
        }

        return Result.Success<string>(s.TrimEnd(trimCharsResult.Value!));
    }

    public static ExpressionDescriptor GetExpressionDescriptor()
        => StringExpression.GetStringTrimDescriptor(
            typeof(TrimEndExpression),
            "Trims the end characters of the expression",
            "String to get the trimmed value for",
            "The trim end value of the expression",
            "This result will be returned when the expression is of type string");
}
