namespace ExpressionFramework.Domain.Aggregators;

[AggregatorDescription("Concatenates two enumerable values")]
[UsesContext(true)]
[ContextDescription("Value to use as context in the aggregator")]
[ContextType(typeof(IEnumerable))]
[ContextRequired(true)]
[ReturnValue(ResultStatus.Ok, typeof(IEnumerable), "Concatenation of two enumerable values", "This will be returned in case the execution returns success (Ok)")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context is not of type enumerable, Second expression is not of type enumerable")]
public partial record EnumerableConcatenateAggregator
{
    public override Result<object?> Aggregate(object? context, Expression secondExpression)
    {
        if (context is not IEnumerable e1)
        {
            return Result<object?>.Invalid("Context is not of type enumerable");
        }

        var secondExpressionResult = secondExpression.Evaluate(context);
        if (!secondExpressionResult.IsSuccessful())
        {
            return secondExpressionResult;
        }

        if (secondExpressionResult.Value is not IEnumerable e2)
        {
            return Result<object?>.Invalid("Second expression is not of type enumerable");
        }

        return Result<object?>.Success(e1.OfType<object?>().Concat(e2.OfType<object?>()));
    }
}

