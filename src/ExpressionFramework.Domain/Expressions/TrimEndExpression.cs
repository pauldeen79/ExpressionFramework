namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(TrimEndExpression))]
public partial record TrimEndExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped<string>(context, "Expression must be of type string").Transform(result =>
            result.IsSuccessful()
                ? TrimEnd(context, result.Value!)
                : result);

    public TrimEndExpression(Expression expression) : this(expression, null) { }

    private Result<string> TrimEnd(object? context, string s)
    {
        if (TrimCharsExpression == null)
        {
            return Result<string>.Success(s.TrimEnd());
        }

        var trimCharsResult = TrimCharsExpression.EvaluateTyped<char[]>(context, "TrimCharsExpression must be of type char[]");
        if (!trimCharsResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(trimCharsResult);
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
}

public partial record TrimEndExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
