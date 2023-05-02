﻿namespace ExpressionFramework.Domain.Aggregators;

[AggregatorDescription("Multiplies two numeric values")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Multiplication of two values", "This will be returned in case the execution returns success (Ok)")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Could not convert FirstExpression to [Type]. Error message: [Error message], Could not convert SecondExpression to [Type]. Error message: [Error message]")]
public partial record MultiplyAggregator
{
    public override Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression)
        => AggregatorBase.Aggregate(context, firstExpression, secondExpression, CultureInfo.InvariantCulture, Multiply.Evaluate);
}

