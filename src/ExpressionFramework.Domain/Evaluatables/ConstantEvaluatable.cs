namespace ExpressionFramework.Domain.Evaluatables;

[EvaluatableDescription("Returns the constant value provided")]
[ParameterDescription(nameof(Value), "Value to use")]
[ParameterRequired(nameof(Value), true)]
[ReturnValue(ResultStatus.Ok, "The value that is supplied with the Value parameter", "This result will always be returned")]
public partial record ConstantEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => Result<bool>.Success(Value);
}

