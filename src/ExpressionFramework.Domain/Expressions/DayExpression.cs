namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the day from the specified DateTime expression")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(int), "Day number (within a month)", "This result will be returned when the expression is of type DateTime")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression is not of type DateTime")]
public partial record DayExpression : ITypedExpression<int>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(Expression.Evaluate(context).TryCast<DateTime>("Expression is not of type DateTime"), x => x.Day);

    public override Result<Expression> GetPrimaryExpression()
        => Result<Expression>.Success(Expression);

    public Result<int> EvaluateTyped(object? context)
        => Result<int>.FromExistingResult(Expression.Evaluate(context).TryCast<DateTime>("Expression is not of type DateTime"), x => x.Day);

    public DayExpression(object? expression) : this(new ConstantExpression(expression)) { }
    public DayExpression(Func<object?, object?> expression) : this(new DelegateExpression(expression)) { }
}

public partial record DayExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
