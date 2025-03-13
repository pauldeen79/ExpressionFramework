namespace ExpressionFramework.Core.Evaluatables;

public class ConstantEvaluatable : IEvaluatable
{
    public bool Value { get; }

    public ConstantEvaluatable(bool value)
    {
        Value = value;
    }

    public Result<bool> Evaluate(object? context)
        => Result.Success(Value);
}
