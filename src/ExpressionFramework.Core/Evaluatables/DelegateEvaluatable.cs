namespace ExpressionFramework.Core.Evaluatables;

public partial record DelegateEvaluatable : IEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => Result.Success(Delegate());
}
