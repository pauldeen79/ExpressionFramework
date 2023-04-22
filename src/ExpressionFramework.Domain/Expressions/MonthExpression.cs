namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the month from the specified DateTime expression")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(int), "Month number", "This result will be returned when the expression is of type DateTime")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression is not of type DateTime")]
public partial record MonthExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(Expression.Evaluate(context).TryCast<DateTime>("Expression is not of type DateTime"), x => x.Month);

    public override Result<Expression> GetPrimaryExpression()
        => Result<Expression>.Success(Expression);

    public Result<int> EvaluateTyped(object? context)
        => Result<int>.FromExistingResult(Expression.Evaluate(context).TryCast<DateTime>("Expression is not of type DateTime"), x => x.Month);

    public MonthExpression(DateTime expression) : this(new TypedConstantExpression<DateTime>(expression)) { }
    public MonthExpression(Func<object?, DateTime> expression) : this(new TypedDelegateExpression<DateTime>(expression)) { }
}
