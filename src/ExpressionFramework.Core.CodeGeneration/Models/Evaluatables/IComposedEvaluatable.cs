namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IComposedEvaluatable : IEvaluatableBase
{
    [Required][ValidateObject] IReadOnlyCollection<IComposableEvaluatable> Conditions { get; }
}
