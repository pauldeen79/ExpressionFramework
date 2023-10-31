namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the month from the specified DateTime expression")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(int), "Month number", "This result will be returned when the expression is of type DateTime")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression is not of type DateTime")]
public partial record MonthExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<DateTime, object?>(Expression.EvaluateTyped(context), x => x.Month);

    public Result<int> EvaluateTyped(object? context)
        => Result.FromExistingResult<DateTime, int>(Expression.EvaluateTyped(context), x => x.Month);
}
