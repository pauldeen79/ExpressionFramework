namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets the current date")]
[ReturnValue(ResultStatus.Ok, typeof(DateTime), "Current date", "This is always returned")]
public partial record TodayExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<DateTime> EvaluateTyped(object? context)
        => Result.Success<DateTime>(DateTimeProvider is null
            ? DateTime.Today
            : DateTimeProvider.GetCurrentDateTime().Date);
}
