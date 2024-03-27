namespace ExpressionFramework.Domain.Aggregators;

[AggregatorDescription("Computes the modulus of two numeric values")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Modulus of two values", "This will be returned in case the execution returns success (Ok)")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Could not convert FirstExpression to [Type]. Error message: [Error message], Could not convert SecondExpression to [Type]. Error message: [Error message]")]
public partial record ModulusAggregator
{
    public override Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression, ITypedExpression<IFormatProvider>? formatProviderExpression)
        => AggregatorBase.Aggregate(context, firstExpression, secondExpression, formatProviderExpression, Modulus.Evaluate);
}
