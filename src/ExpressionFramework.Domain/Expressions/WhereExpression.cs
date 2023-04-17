namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Filters an enumerable context value using a predicate")]
[ContextDescription("Value to use as context in the expression")]
[ContextType(typeof(IEnumerable))]
[ParameterDescription(nameof(PredicateExpression), "Predicate to apply to each value. Return value must be a boolean value, so we can filter on it")]
[ParameterRequired(nameof(PredicateExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with items that satisfy the predicate", "This result will be returned when the context is enumerable, and the predicate returns a boolean value")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression is not of type enumerable, Predicate did not return a boolean value")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case predicate evaluation fails")]
public partial record WhereExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetResultFromEnumerable(Expression, context, e => e
            .Select(x => new { Item = x, Result = GetResult(PredicateExpression.Evaluate(x)) })
            .Where(x => !x.Result.IsSuccessful() || x.Result.Value.IsTrue())
            .Select(x => x.Result.IsSuccessful()
                ? Result<object?>.Success(x.Item)
                : x.Result));

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression);

    private Result<object?> GetResult(Result<object?> itemResult)
    {
        if (!itemResult.IsSuccessful())
        {
            return itemResult;
        }

        if (itemResult.Value is not bool)
        {
            return Result<object?>.Invalid("PredicateExpression did not return a boolean value");
        }

        return itemResult;
    }

    public WhereExpression(object? expression, Func<object?, object?> predicateExpression) : this(new ConstantExpression(expression), new DelegateExpression(predicateExpression)) { }
    public WhereExpression(Func<object?, object?> expression, Func<object?, object?> predicateExpression) : this(new DelegateExpression(expression), new DelegateExpression(predicateExpression)) { }
}

public partial record WhereExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
