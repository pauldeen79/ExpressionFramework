namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(TrimEndExpression))]
public partial record TrimEndExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression.ToUntyped());

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped(context).Transform(result =>
            result.IsSuccessful()
                ? TrimEnd(context, result.Value)
                : result);

    private Result<string> TrimEnd(object? context, string? s)
    {
        if (s is null)
        {
            return Result<string>.Invalid("Expression is not of type string");
        }

        if (TrimCharsExpression is null)
        {
            return Result<string>.Success(s.TrimEnd());
        }

        var trimCharsResult = TrimCharsExpression.EvaluateTyped(context);
        if (!trimCharsResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(trimCharsResult);
        }

        if (trimCharsResult.Value == null)
        {
            return Result<string>.Success(s.TrimEnd());
        }

        return Result<string>.Success(s.TrimEnd(trimCharsResult.Value!));
    }

    public static ExpressionDescriptor GetExpressionDescriptor()
        => StringExpression.GetStringTrimDescriptor(
            typeof(TrimEndExpression),
            "Trims the end characters of the expression",
            "String to get the trimmed value for",
            "The trim end value of the expression",
            "This result will be returned when the expression is of type string");

    public TrimEndExpression(string expression) : this(new TypedConstantExpression<string>(expression), null) { }
}
