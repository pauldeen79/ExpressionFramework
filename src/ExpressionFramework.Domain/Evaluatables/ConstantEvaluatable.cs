namespace ExpressionFramework.Domain.Evaluatables;

[EvaluatableDescription("Returns the constant value provided")]
[EvaluatableParameterDescription(nameof(Value), "Value to use")]
[EvaluatableParameterRequired(nameof(Value), true)]
[EvaluatableReturnValue(ResultStatus.Ok, "The value that is supplied with the Value parameter", "This result will always be returned")]
public partial record ConstantEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => Result<bool>.Success(Value);
}

