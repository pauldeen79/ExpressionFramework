﻿namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(TrimStartExpression))]
public partial record TrimStartExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped<string>(context, "Expression must be of type string").Transform(result =>
            result.IsSuccessful()
                ? TrimStart(context, result.Value!)
                : result);

    public TrimStartExpression(Expression expression) : this(expression, null) { }

    private Result<string> TrimStart(object? context, string s)
    {
        if (TrimCharsExpression == null)
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
}

