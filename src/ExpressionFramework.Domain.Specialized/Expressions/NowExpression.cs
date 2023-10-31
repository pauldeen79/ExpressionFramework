namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets the current date and time")]
[ReturnValue(ResultStatus.Ok, typeof(DateTime), "Current date and time", "This is always returned")]
public partial record NowExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<DateTime> EvaluateTyped(object? context)
        => Result.Success<DateTime>(DateTimeProvider is null
            ? DateTime.Now
            : DateTimeProvider.GetCurrentDateTime());
}
