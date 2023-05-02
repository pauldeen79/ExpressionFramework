namespace ExpressionFramework.Domain.Aggregators;

[AggregatorDescription("Divides two numeric values")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(object), "First value divided by second value", "This will be returned in case the execution returns success (Ok)")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Could not convert FirstExpression to [Type]. Error message: [Error message], Could not convert SecondExpression to [Type]. Error message: [Error message]")]
public partial record DivideAggregator
{
    public override Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression)
        => AggregatorBase.Aggregate(context, firstExpression, secondExpression, CultureInfo.InvariantCulture, Divide.Evaluate);
}

