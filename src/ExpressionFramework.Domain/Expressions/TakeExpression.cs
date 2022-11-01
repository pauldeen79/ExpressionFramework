namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Takes a number of items from an enumerable context value")]
[ContextDescription("The enumerable value to take elements from")]
[ContextType(typeof(IEnumerable))]
[ParameterDescription(nameof(CountExpression), "Number of items to take")]
[ParameterRequired(nameof(CountExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with taken items", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "CountExpression is not of type integer, Expression is not of type IEnumerable")]
public partial record TakeExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var countResult = CountExpression.EvaluateTyped<int>(context, "CountExpression is not of type integer");
        if (!countResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(countResult);
        }

        var enumerableResult = Expression.EvaluateTyped<IEnumerable>(context, "Expression is not of type enumerable");
        if (!enumerableResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(enumerableResult);
        }

        return EnumerableExpression.GetResultFromEnumerable(enumerableResult.Value!, e => e
            .Take(countResult.Value)
            .Select(x => Result<object?>.Success(x)));
    }

    public TakeExpression(IEnumerable enumerable, int count) : this(new ConstantExpression(enumerable), new ConstantExpression(count)) { }
}

