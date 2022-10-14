﻿namespace ExpressionFramework.Domain.Evaluatables;

[EvaluatableDescription("Evaluates one condition")]
[ParameterDescription(nameof(LeftExpression), "Left expression")]
[ParameterRequired(nameof(LeftExpression), true)]
[ParameterDescription(nameof(Operator), "Operator to use")]
[ParameterRequired(nameof(Operator), true)]
[ParameterDescription(nameof(RightExpression), "Right expression")]
[ParameterRequired(nameof(RightExpression), true)]
[ReturnValue(ResultStatus.Ok, "true when the condition evaluates to true, otherwise false", "This result will be returned when evaluation of the expressions succeed")]
public partial record SingleEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => Operator.Evaluate(context, LeftExpression, RightExpression);

    public SingleEvaluatable(object leftValue, Operator @operator, object rightValue)
        : this(new ConstantExpression(leftValue), @operator, new ConstantExpression(rightValue)) { }
}
