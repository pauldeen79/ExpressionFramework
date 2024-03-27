namespace ExpressionFramework.Domain.Evaluatables;

[EvaluatableDescription("Returns the delegate value provided")]
[ParameterDescription(nameof(Value), "Value to use")]
[ParameterRequired(nameof(Value), true)]
[ReturnValue(ResultStatus.Ok, "The value that is returned from the supplied delegate using the Value parameter", "This result will always be returned")]
public partial record DelegateEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => Result.Success(Value.Invoke());
}
