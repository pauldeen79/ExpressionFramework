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

        var enumerableResult = Expression.EvaluateTyped<IEnumerable>(context, "Expression is not of type enumerable");
        if (!enumerableResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(enumerableResult);
        }

        return EnumerableExpression.GetResultFromEnumerable(enumerableResult.Value!, e => e
            .Skip(countResult.Value)
            .Select(x => Result<object?>.Success(x)));
    }
}
