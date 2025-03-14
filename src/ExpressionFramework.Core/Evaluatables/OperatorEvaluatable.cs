namespace ExpressionFramework.Core.Evaluatables;

public partial record OperatorEvaluatable : IEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => Operator.Evaluate(LeftValue, RightValue, StringComparison);
}
