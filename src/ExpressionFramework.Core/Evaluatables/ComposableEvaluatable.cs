namespace ExpressionFramework.Core.Evaluatables;

public partial record ComposableEvaluatable : IEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => InnerEvaluatable.Evaluate(context);
}
