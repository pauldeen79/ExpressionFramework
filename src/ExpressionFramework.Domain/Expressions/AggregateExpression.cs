namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Aggregates context with other expressions")]
[ContextDescription("Value to use as context in the aggregator")]
[ContextType(typeof(object))]
[ParameterDescription(nameof(Aggregator), "Aggregator to evaluate")]
[ParameterRequired(nameof(Aggregator), true)]
[ParameterDescription(nameof(FirstExpression), "Expression to use as seed in aggregator")]
[ParameterRequired(nameof(FirstExpression), true)]
[ParameterType(nameof(FirstExpression), typeof(object))]
[ParameterDescription(nameof(SubsequentExpressions), "Expressions to use as subsequent expression in aggregator")]
[ParameterRequired(nameof(SubsequentExpressions), true)]
[ParameterType(nameof(SubsequentExpressions), typeof(object))]
[ReturnValue(ResultStatus.Ok, typeof(object), "Result value of the last expression", "This will be returned in case the aggregator returns success (Ok)")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case the aggregator returns something else than Ok")]
public partial record AggregateExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var result = FirstExpression.Evaluate(context);
        if (!result.IsSuccessful())
        {
            return result;
        }
        foreach (var expression in SubsequentExpressions)
        {
            result = Aggregator.Aggregate(context, new ConstantExpression(result.Value), expression);
            if (!result.IsSuccessful())
            {
                return result;
            }
        }
        return result;
    }

    public AggregateExpression(IEnumerable<object?> values, Aggregator aggregator)
        : this(new ConstantExpression(values.First()), values.Skip(1).Select(x => new ConstantExpression(x)), aggregator) { }
}

