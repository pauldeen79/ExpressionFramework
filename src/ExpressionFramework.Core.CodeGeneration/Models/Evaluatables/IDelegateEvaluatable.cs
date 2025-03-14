namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IDelegateEvaluatable : IEvaluatableBase
{
    [Required][ValidateObject] Func<bool> Delegate { get; }
}
