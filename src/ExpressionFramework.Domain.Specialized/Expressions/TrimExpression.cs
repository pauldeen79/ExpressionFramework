﻿namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(TrimExpression))]
public partial record TrimExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTyped(context).Transform(result =>
            result.IsSuccessful()
                ? Trim(context, result.Value)
                : result);

    private Result<string> Trim(object? context, string? s)
    {
        if (s is null)
        {
            return Result.Invalid<string>("Expression is not of type string");
        }

        if (TrimCharsExpression is null)
        {
            return Result.Success(s.Trim());
        }

        var trimCharsResult = TrimCharsExpression.EvaluateTyped(context);
        if (!trimCharsResult.IsSuccessful())
        {
            return Result.FromExistingResult<string>(trimCharsResult);
        }

        if (trimCharsResult.Value is null)
        {
            return Result.Success(s.Trim());
        }

        return Result.Success(s.Trim(trimCharsResult.Value));
    }

    public static ExpressionDescriptor GetExpressionDescriptor()
        => StringExpression.GetStringTrimDescriptor(
            typeof(TrimExpression),
            "Trims the start and end characters of the expression",
            "String to get the trimmed value for",
            "The trim start and end value of the expression",
            "This result will be returned when the expression is of type string");
}
