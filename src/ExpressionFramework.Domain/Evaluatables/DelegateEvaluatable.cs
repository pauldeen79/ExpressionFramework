namespace ExpressionFramework.Domain.Evaluatables;

[EvaluatableDescription("Returns the delegate value provided")]
[EvaluatableParameterDescription(nameof(Value), "Value to use")]
[EvaluatableParameterRequired(nameof(Value), true)]
[EvaluatableReturnValue(ResultStatus.Ok, "The value that is returned from the supplied delegate using the Value parameter", "This result will always be returned")]
public partial record DelegateEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => Result<bool>.Success(Value.Invoke());
}
