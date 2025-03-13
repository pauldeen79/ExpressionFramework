namespace ExpressionFramework.Core.Evaluatables;

public class DelegateEvaluatable : IEvaluatable
{
    public Func<bool> Delegate { get; }

    public DelegateEvaluatable(Func<bool> @delegate)
    {
        Delegate = @delegate;
    }

    public Result<bool> Evaluate(object? context)
        => Result.Success(Delegate());
}
