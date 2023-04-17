namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(TrimStartExpression))]
public partial record TrimStartExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped<string>(context, "Expression must be of type string").Transform(result =>
            result.IsSuccessful()
                ? TrimStart(context, result.Value!)
                : result);

    public Expression ToUntyped() => this;

    private Result<string> TrimStart(object? context, string s)
    {
        if (TrimCharsExpression is null)
        {
            return Result<string>.Success(s.TrimStart());
        }

        var trimCharsResult = TrimCharsExpression.EvaluateTyped<char[]>(context, "TrimCharsExpression must be of type char[]");
        if (!trimCharsResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(trimCharsResult);
        }

        return Result<string>.Success(s.TrimStart(trimCharsResult.Value!));
    }

    public static ExpressionDescriptor GetExpressionDescriptor()
        => StringExpression.GetStringTrimDescriptor(
            typeof(TrimStartExpression),
            "Trims the start characters of the expression",
            "String to get the trimmed value for",
            "The trim start value of the expression",
            "This result will be returned when the expression is of type string");

    public TrimStartExpression(object? expression, object? trimCharsExpression = null) : this(new ConstantExpression(expression), trimCharsExpression == null ? null : new ConstantExpression(trimCharsExpression)) { }
    public TrimStartExpression(Func<object?, object?> expression, Func<object?, object?>? trimCharsExpression = null) : this(new DelegateExpression(expression), trimCharsExpression == null ? null : new DelegateExpression(trimCharsExpression)) { }
}

public partial record TrimStartExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
