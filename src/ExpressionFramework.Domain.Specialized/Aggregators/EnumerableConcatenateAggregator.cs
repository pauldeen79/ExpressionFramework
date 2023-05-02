namespace ExpressionFramework.Domain.Aggregators;

[AggregatorDescription("Concatenates two enumerable values")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Concatenation of two enumerable values", "This will be returned in case the execution returns success (Ok)")]
public partial record EnumerableConcatenateAggregator
{
    public override Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression, ITypedExpression<IFormatProvider>? formatProviderExpression)
    {
        var result1 = firstExpression.EvaluateTyped<IEnumerable>(context, "First expression is not of type enumerable");
        if (!result1.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(result1);
        }

        var result2 = secondExpression.EvaluateTyped<IEnumerable>(result1.Value, "Second expression is not of type enumerable");
        if (!result2.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(result2);
        }

        return Result<object?>.Success(result1.Value.OfType<object?>().Concat(result2.Value.OfType<object?>()));
    }
}

