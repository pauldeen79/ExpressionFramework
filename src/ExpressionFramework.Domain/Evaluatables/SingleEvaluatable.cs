namespace ExpressionFramework.Domain.Evaluatables;

[EvaluatableDescription("Evaluates one condition")]
[EvaluatableParameterDescription(nameof(LeftExpression), "Left expression")]
[EvaluatableParameterRequired(nameof(LeftExpression), true)]
[EvaluatableParameterDescription(nameof(Operator), "Operator to use")]
[EvaluatableParameterRequired(nameof(Operator), true)]
[EvaluatableParameterDescription(nameof(RightExpression), "Right expression")]
[EvaluatableParameterRequired(nameof(RightExpression), true)]
[EvaluatableReturnValue(ResultStatus.Ok, "true when the condition evaluates to true, otherwise false", "This result will be returned when evaluation of the expressions succeed")]
public partial record SingleEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => Operator.Evaluate(context, LeftExpression, RightExpression);
}
