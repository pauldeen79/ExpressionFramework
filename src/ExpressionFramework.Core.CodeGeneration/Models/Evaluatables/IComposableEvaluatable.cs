namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IComposableEvaluatable : IEvaluatableBase
{
    [Required][ValidateObject] Abstractions.IEvaluatable InnerEvaluatable { get; }

    Combination? Combination { get; }
    bool StartGroup { get; }
    bool EndGroup { get; }
}
