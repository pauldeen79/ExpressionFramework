namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets the current date and time")]
[ReturnValue(ResultStatus.Ok, typeof(DateTime), "Current date and time", "This is always returned")]
public partial record NowExpression : ITypedExpression<DateTime>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<DateTime> EvaluateTyped(object? context)
        => Result<DateTime>.Success(DateTimeProvider == null
            ? DateTime.Now
            : DateTimeProvider.GetCurrentDateTime());

    public NowExpression() : this(default(IDateTimeProvider))
    {
    }
}

public partial record NowExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
