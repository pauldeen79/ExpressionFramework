namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IDelegateEvaluatable : IEvaluatable
{
    [Required][ValidateObject] Func<bool> Value { get; }
}
