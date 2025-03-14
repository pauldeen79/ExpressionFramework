namespace ExpressionFramework.Core.Evaluatables;

public partial record ConstantEvaluatable : IEvaluatable
{
    public override Result<bool> Evaluate(object? context)
        => Result.Success(Value);
}
