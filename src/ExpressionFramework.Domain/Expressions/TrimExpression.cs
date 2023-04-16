namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(TrimExpression))]
public partial record TrimExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped<string>(context, "Expression must be of type string").Transform(result =>
            result.IsSuccessful()
                ? Trim(context, result.Value!)
                : result);

    public TrimExpression(Expression expression) : this(expression, null) { }

    private Result<string> Trim(object? context, string s)
    {
        if (TrimCharsExpression == null)
        {
            return Result<string>.Success(s.Trim());
        }

        var trimCharsResult = TrimCharsExpression.EvaluateTyped<char[]>(context, "TrimCharsExpression must be of type char[]");
        if (!trimCharsResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(trimCharsResult);
        }

        return Result<string>.Success(s.Trim(trimCharsResult.Value!));
    }

    public static ExpressionDescriptor GetExpressionDescriptor()
        => StringExpression.GetStringTrimDescriptor(
            typeof(TrimExpression),
            "Trims the start and end characters of the expression",
            "String to get the trimmed value for",
            "The trim start and end value of the expression",
            "This result will be returned when the expression is of type string");
}

public partial record TrimExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
