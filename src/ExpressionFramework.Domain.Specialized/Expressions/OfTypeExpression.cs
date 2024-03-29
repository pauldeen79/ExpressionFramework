﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Filters an enumerable context value on type")]
[ContextDescription("Value to use as context in the expression")]
[ContextType(typeof(IEnumerable))]
[ParameterDescription(nameof(TypeExpression), "Type to filter on")]
[ParameterRequired(nameof(TypeExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with items that are of the specified type", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression is not of type enumerable")]
public partial record OfTypeExpression
{
    public override Result<object?> Evaluate(object? context) => Result.FromExistingResult<IEnumerable<object?>, object?>(EvaluateTyped(context), result => result);

    public Result<IEnumerable<object?>> EvaluateTyped(object? context)
    {
        var typeResult = TypeExpression.EvaluateTyped(context);
        if (!typeResult.IsSuccessful())
        {
            return Result.FromExistingResult<IEnumerable<object?>>(typeResult);
        }

        return EnumerableExpression.GetTypedResultFromEnumerable(Expression, context, e => IsOfType(e, typeResult));
    }

    private static IEnumerable<Result<object?>> IsOfType(IEnumerable<object?> e, Result<Type> typeResult) => e
        .Where(x => x is not null && typeResult.Value!.IsInstanceOfType(x))
        .Select(Result.Success);
}
