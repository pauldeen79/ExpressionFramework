namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IDelegateResultEvaluatable : IEvaluatableBase
{
    [Required][ValidateObject] Func<Result<bool>> Delegate { get; }
}
