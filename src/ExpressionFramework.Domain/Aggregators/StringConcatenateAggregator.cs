namespace ExpressionFramework.Domain.Aggregators;

[AggregatorDescription("Concatenates two string values")]
[UsesContext(true)]
[ContextDescription("Value to use as context in the aggregator")]
[ContextType(typeof(string))]
[ContextRequired(true)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Concatenation of two string values", "This will be returned in case the execution returns success (Ok)")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context is not of type string, Second expression is not of type string")]
public partial record StringConcatenateAggregator
{
    public override Result<object?> Aggregate(object? context, Expression secondExpression)
    {
        if (context is not string s1)
        {
            return Result<object?>.Invalid("Context is not of type string");
        }

        var secondExpressionResult = secondExpression.Evaluate(context);
        if (!secondExpressionResult.IsSuccessful())
        {
            return secondExpressionResult;
        }

        if (secondExpressionResult.Value is not string s2)
        {
            return Result<object?>.Invalid("Second expression is not of type string");
        }

        return Result<object?>.Success(s1 + s2);
    }
}

