namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the year from the specified DateTime expression")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(int), "Year", "This result will be returned when the expression is of type DateTime")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression is not of type DateTime")]
public partial record YearExpression : ITypedExpression<int>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(Expression.Evaluate(context).TryCast<DateTime>("Expression is not of type DateTime"), x => x.Year);

    public override Result<Expression> GetPrimaryExpression()
        => Result<Expression>.Success(Expression);

    public Result<int> EvaluateTyped(object? context)
        => Result<int>.FromExistingResult(Expression.Evaluate(context).TryCast<DateTime>("Expression is not of type DateTime"), x => x.Year);

    public YearExpression(object? expression) : this(new ConstantExpression(expression)) { }
    public YearExpression(Func<object?, object?> expression) : this(new DelegateExpression(expression)) { }
}

public partial record YearExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
