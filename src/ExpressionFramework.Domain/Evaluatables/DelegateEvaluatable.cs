namespace ExpressionFramework.Domain.Evaluatables;

public partial record DelegateEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => Result<bool>.Success(Value.Invoke());
}
