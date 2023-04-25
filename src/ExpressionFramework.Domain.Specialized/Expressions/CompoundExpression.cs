namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Executes an aggregator")]
[UsesContext(true)]
[ContextDescription("Value to use as context in expressions and the aggregator")]
[ContextType(typeof(object))]
[ContextRequired(false)]
[ParameterDescription(nameof(Aggregator), "Aggregator to evaluate")]
[ParameterRequired(nameof(Aggregator), true)]
[ParameterDescription(nameof(SecondExpression), "Expression to use as second expression in aggregator")]
[ParameterRequired(nameof(SecondExpression), true)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Result value of the last expression", "This will be returned in case the aggregator returns success (Ok)")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case the aggregator returns something else than Ok")]
public partial record CompoundExpression
{
    public override Result<object?> Evaluate(object? context)
        => Aggregator.Aggregate(context, FirstExpression, SecondExpression);
}
