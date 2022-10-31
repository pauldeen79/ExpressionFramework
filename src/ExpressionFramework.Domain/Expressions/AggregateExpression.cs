namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Aggregates context with other expressions")]
[UsesContext(true)]
[ContextDescription("Value to use as context in expressions and the aggregator")]
[ContextType(typeof(object))]
[ContextRequired(false)]
[ParameterDescription(nameof(Aggregator), "Aggregator to evaluate")]
[ParameterRequired(nameof(Aggregator), true)]
[ParameterDescription(nameof(Expressions), "Expressions to use in aggregator")]
[ParameterRequired(nameof(Expressions), true)]
[ParameterType(nameof(Expressions), typeof(object))]
[ReturnValue(ResultStatus.Ok, typeof(object), "Result value of the last expression", "This will be returned in case the aggregator returns success (Ok)")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Sequence contains no elements")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case the aggregator returns something else than Ok")]
public partial record AggregateExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        if (!Expressions.Any())
        {
            return Result<object?>.Invalid("Sequence contains no elements");
        }

        var result = Expressions.First().Evaluate(context);
        if (!result.IsSuccessful())
        {
            return result;
        }

        foreach (var expression in Expressions.Skip(1))
        {
            result = Aggregator.Aggregate(context, new ConstantExpression(result.Value), expression);
            if (!result.IsSuccessful())
            {
                return result;
            }
        }

        return result;
    }
}

