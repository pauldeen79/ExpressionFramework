﻿namespace ExpressionFramework.Domain.Expressions;

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
        => EnumerableExpression.GetResultFromEnumerable(Expression, context, Filter);

    public Result<IEnumerable<object?>> EvaluateTyped(object? context)
        => EnumerableExpression.GetTypedResultFromEnumerable(Expression, context, Filter);

    private IEnumerable<Result<object?>> Filter(IEnumerable<object?> e) => e
        .Select(x => new { Item = x, Result = PredicateExpression.EvaluateTyped(x) })
        .Where(x => !x.Result.IsSuccessful() || x.Result.Value.IsTrue())
        .Select(x => x.Result.IsSuccessful()
            ? Result.Success(x.Item)
            : Result.FromExistingResult<object?>(x.Result));
}
