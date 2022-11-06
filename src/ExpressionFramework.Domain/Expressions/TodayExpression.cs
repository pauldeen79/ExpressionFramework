namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets the current date")]
[ReturnValue(ResultStatus.Ok, typeof(DateTime), "Current date", "This is always returned")]
public partial record TodayExpression : ITypedExpression<DateTime>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<DateTime> EvaluateTyped(object? context)
        => Result<DateTime>.Success(DateTimeProvider == null ? DateTime.Today : DateTimeProvider.GetCurrentDateTime().Date);
}

