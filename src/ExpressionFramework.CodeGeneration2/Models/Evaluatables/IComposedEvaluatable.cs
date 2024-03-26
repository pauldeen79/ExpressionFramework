namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IComposedEvaluatable : IEvaluatable
{
    [Required][ValidateObject] IReadOnlyCollection<IComposableEvaluatable> Conditions { get; }
}
