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
    public override Result<object?> Evaluate(object? context) => Result<object?>.FromExistingResult(EvaluateTyped(context), result => result);

    public override Result<Expression> GetPrimaryExpression() => Result<Expression>.Success(Expression.ToUntyped());

    public Result<IEnumerable<object?>> EvaluateTyped(object? context) => EnumerableExpression.GetTypedResultFromEnumerableWithCount(Expression, CountExpression, context, Skip);

    private static IEnumerable<Result<object?>> Skip(IEnumerable<object?> e, Result<int> countResult) => e
        .Skip(countResult.Value)
        .Select(x => Result<object?>.Success(x));
}
