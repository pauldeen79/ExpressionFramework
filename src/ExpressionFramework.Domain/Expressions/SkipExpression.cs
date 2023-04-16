namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Skips a number of items on an enumerable context value")]
[ContextType(typeof(IEnumerable))]
[ContextDescription("The enumerable value to skip elements from")]
[ContextRequired(true)]
[ParameterDescription(nameof(CountExpression), "Number of items to skip")]
[ParameterRequired(nameof(CountExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with skipped items", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "CountExpression is not of type integer, Expression is not of type IEnumerable")]
public partial record SkipExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var countResult = CountExpression.EvaluateTyped<int>(context, "CountExpression is not of type integer");
        if (!countResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(countResult);
        }

        return EnumerableExpression.GetResultFromEnumerable(Expression, context, e => e
            .Skip(countResult.Value)
            .Select(x => Result<object?>.Success(x)));
    }

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public SkipExpression(object? expression, int count) : this(new ConstantExpression(expression), new ConstantExpression(count)) { }
    public SkipExpression(Func<object?, object?> expression, Func<object?, int> countDelegate) : this(new DelegateExpression(expression), new TypedDelegateExpression<int>(countDelegate)) { }
}

public partial record SkipExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
