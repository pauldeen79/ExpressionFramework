namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the year from the specified DateTime expression")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(int), "Year", "This result will be returned when the expression is of type DateTime")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression is not of type DateTime")]
public partial record YearExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(Expression.EvaluateTyped(context), x => x.Year);

    public override Result<Expression> GetPrimaryExpression()
        => Result<Expression>.Success(Expression.ToUntyped());

    public Result<int> EvaluateTyped(object? context)
        => Result<int>.FromExistingResult(Expression.EvaluateTyped(context), x => x.Year);
}
