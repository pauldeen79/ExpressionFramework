namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Aggregates context with other expressions")]
[UsesContext(true)]
[ContextDescription("Value to use as context in the aggregator")]
[ContextType(typeof(object))]
[ContextRequired(true)]
[ParameterDescription(nameof(Aggregator), "Aggregator to evaluate")]
[ParameterRequired(nameof(Aggregator), true)]
[ParameterDescription(nameof(SubsequentExpressions), "Expressions to use as subsequent expression in aggregator")]
[ParameterRequired(nameof(SubsequentExpressions), true)]
[ParameterType(nameof(SubsequentExpressions), typeof(object))]
[ReturnValue(ResultStatus.Ok, typeof(object), "Result value of the last expression", "This will be returned in case the aggregator returns success (Ok)")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case the aggregator returns something else than Ok")]
public partial record AggregateExpression
{
    public override Result<object?> Evaluate(object? context)
        => SubsequentExpressions.Aggregate(Result<object?>.Success(context), (seed, accumulator)
            => !seed.IsSuccessful()
                ? seed
                : Aggregator.Aggregate(seed.Value, accumulator));

    public AggregateExpression(IEnumerable<object?> subsequentValues, Aggregator aggregator)
        : this(subsequentValues.Select(x => new ConstantExpression(x)), aggregator) { }
}

