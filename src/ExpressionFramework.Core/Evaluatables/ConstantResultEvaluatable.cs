namespace ExpressionFramework.Core.Evaluatables;

public partial record ConstantResultEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => Result;
}
