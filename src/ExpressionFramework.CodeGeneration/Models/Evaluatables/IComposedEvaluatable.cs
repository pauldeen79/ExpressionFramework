namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IComposedEvaluatable : IEvaluatable
{
    [Required]
    IReadOnlyCollection<ISingleEvaluatable> Conditions { get; }
}
