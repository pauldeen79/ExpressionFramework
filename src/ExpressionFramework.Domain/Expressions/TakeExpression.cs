namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Takes a number of items from an enumerable context value")]
[ContextDescription("The enumerable value to take elements from")]
[ContextType(typeof(IEnumerable))]
[ParameterDescription(nameof(CountExpression), "Number of items to take")]
[ParameterRequired(nameof(CountExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with taken items", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "CountExpression is not of type integer, Expression is not of type IEnumerable")]
public partial record TakeExpression : ITypedExpression<IEnumerable<object?>>
{
    public override Result<object?> Evaluate(object? context) => Result<object?>.FromExistingResult(EvaluateTyped(context), result => result);

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    public Result<IEnumerable<object?>> EvaluateTyped(object? context)
    {
        var countResult = CountExpression.EvaluateTyped<int>(context, "CountExpression is not of type integer");
        if (!countResult.IsSuccessful())
        {
            return Result<IEnumerable<object?>>.FromExistingResult(countResult);
        }

        return EnumerableExpression.GetTypedResultFromEnumerable(Expression, context, e => Take(e, countResult));
    }

    public Expression ToUntyped() => this;

    public TakeExpression(object? expression, int count) : this(new ConstantExpression(expression), new ConstantExpression(count)) { }
    public TakeExpression(Func<object?, object?> expression, Func<object?, int> countDelegate) : this(new DelegateExpression(expression), new TypedDelegateExpression<int>(countDelegate)) { }

    private static IEnumerable<Result<object?>> Take(IEnumerable<object?> e, Result<int> countResult) => e
        .Take(countResult.Value)
        .Select(x => Result<object?>.Success(x));
}

public partial record TakeExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
