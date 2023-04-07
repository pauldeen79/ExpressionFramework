namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Filters an enumerable context value on type")]
[ContextDescription("Value to use as context in the expression")]
[ContextType(typeof(IEnumerable))]
[ParameterDescription(nameof(TypeExpression), "Type to filter on")]
[ParameterRequired(nameof(TypeExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Enumerable with items that are of the specified type", "This result will be returned when the context is enumerable")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression is not of type enumerable")]
public partial record OfTypeExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var typeResult = TypeExpression.EvaluateTyped<Type>(context, "TypeExpression is not of type Type");
        if (!typeResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(typeResult);
        }

        return EnumerableExpression.GetResultFromEnumerable(Expression, context, e => e
            .Where(x => x != null && typeResult.Value!.IsInstanceOfType(x))
            .Select(x => Result<object?>.Success(x)));
    }
}

public partial record OfTypeExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
