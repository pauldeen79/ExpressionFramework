namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IDelegateEvaluatable : IEvaluatable
{
    Func<bool> Value { get; }
}
