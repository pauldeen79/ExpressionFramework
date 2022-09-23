namespace ExpressionFramework.Domain.Evaluatables;

public partial record ConstantEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => Result<bool>.Success(Value);
}

