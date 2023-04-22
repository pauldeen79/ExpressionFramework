﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets the current date and time")]
[ReturnValue(ResultStatus.Ok, typeof(DateTime), "Current date and time", "This is always returned")]
public partial record NowExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<DateTime> EvaluateTyped(object? context)
        => Result<DateTime>.Success(DateTimeProvider is null
            ? DateTime.Now
            : DateTimeProvider.GetCurrentDateTime());

    public NowExpression() : this(default(IDateTimeProvider))
    {
    }
}
