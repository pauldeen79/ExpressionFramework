namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(TrimStartExpression))]
public partial record TrimStartExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context));

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression.ToUntyped());

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped(context).Transform(result =>
            result.IsSuccessful()
                ? TrimStart(context, result.Value!)
                : result);

    private Result<string> TrimStart(object? context, string s)
    {
        if (s is null)
        {
            return Result<string>.Invalid("Expression is not of type string");
        }

        if (TrimCharsExpression is null)
        {
            return Result<string>.Success(s.TrimStart());
        }

        var trimCharsResult = TrimCharsExpression.EvaluateTyped(context);
        if (!trimCharsResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(trimCharsResult);
        }

        if (trimCharsResult.Value is null)
        {
            return Result<string>.Success(s.TrimStart());
        }

        return Result<string>.Success(s.TrimStart(trimCharsResult.Value));
    }

    public static ExpressionDescriptor GetExpressionDescriptor()
        => StringExpression.GetStringTrimDescriptor(
            typeof(TrimStartExpression),
            "Trims the start characters of the expression",
            "String to get the trimmed value for",
            "The trim start value of the expression",
            "This result will be returned when the expression is of type string");
}
