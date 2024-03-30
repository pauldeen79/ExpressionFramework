namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the day from the specified DateTime expression")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(int), "Day number (within a month)", "This result will be returned when the expression is of type DateTime")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression is not of type DateTime")]
public partial record DayExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<DateTime, object?>(Expression.EvaluateTyped(context), x => x.Day);

    public Result<int> EvaluateTyped(object? context)
        => Result.FromExistingResult(Expression.EvaluateTyped(context), x => x.Day);
}
