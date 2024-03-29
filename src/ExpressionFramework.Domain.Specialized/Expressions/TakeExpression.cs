﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Takes a number of items from an enumerable context value")]
[ContextDescription("The enumerable value to take elements from")]
[ContextType(typeof(IEnumerable))]
[ParameterDescription(nameof(CountExpression), "Number of items to take")]
[ParameterRequired(nameof(CountExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with taken items", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "CountExpression is not of type integer, Expression is not of type IEnumerable")]
public partial record TakeExpression
{
    public override Result<object?> Evaluate(object? context) => Result.FromExistingResult<IEnumerable<object?>, object?>(EvaluateTyped(context), result => result);

    public Result<IEnumerable<object?>> EvaluateTyped(object? context) => EnumerableExpression.GetTypedResultFromEnumerableWithCount(Expression, CountExpression, context, Take);

    private static IEnumerable<Result<object?>> Take(IEnumerable<object?> e, Result<int> countResult) => e
        .Take(countResult.Value)
        .Select(Result.Success);
}
