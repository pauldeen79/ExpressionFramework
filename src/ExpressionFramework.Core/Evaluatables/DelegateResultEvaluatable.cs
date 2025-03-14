namespace ExpressionFramework.Core.Evaluatables;

public partial record DelegateResultEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => Delegate();
}
