namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IDelegateResultEvaluatable : IEvaluatableBase
{
    [Required][ValidateObject] Func<object?, Result<bool>> Delegate { get; }
}
