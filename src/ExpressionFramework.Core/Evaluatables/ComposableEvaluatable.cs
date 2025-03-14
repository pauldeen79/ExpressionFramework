namespace ExpressionFramework.Core.Evaluatables;

public class ComposableEvaluatable : IEvaluatable
{
    public IEvaluatable InnerEvaluatable { get; }
    public Combination? Combination { get; }
    public bool StartGroup { get; }
    public bool EndGroup { get; }

    public ComposableEvaluatable(IEvaluatable innerEvaluatable, Combination? combination, bool startGroup, bool endGroup)
    {
        InnerEvaluatable = innerEvaluatable;
        Combination = combination;
        StartGroup = startGroup;
        EndGroup = endGroup;
    }

    public Result<bool> Evaluate(object? context)
        => InnerEvaluatable.Evaluate(context);
}
